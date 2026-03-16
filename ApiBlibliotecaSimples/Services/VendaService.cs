using ApiBibliotecaSimples.Domain.Entities;
using ApiBlibliotecaSimples.DTOs;
using ApiBlibliotecaSimples.Exceptions;
using ApiBlibliotecaSimples.Interfaces;
using AutoMapper;
using static System.Net.Mime.MediaTypeNames;

namespace ApiBlibliotecaSimples.Services;

public class VendaService : IVendaService
{
    private readonly IVendaRepository _vendaRepository;
    private readonly IExemplarRepository _exemplarRepository;
    private readonly IMapper _mapper;

    public VendaService(IVendaRepository vendaRepository, IExemplarRepository exemplarRepository, IMapper mapper)
    {
        _vendaRepository = vendaRepository;
        _exemplarRepository = exemplarRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DtoResponseVenda>> ObterVendas()
    {
        var vendas = await _vendaRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<DtoResponseVenda>>(vendas);
    }

    public async Task<DtoResponseVenda> ObterVendaPorId(int id)
    {
        var venda = await _vendaRepository.GetByIdAsync(id);
        if (venda == null) throw new NotFoundException("Venda não encontrada");
        return _mapper.Map<DtoResponseVenda>(venda);
    }

    public async Task<DtoResponseVendaComItens> ObterVendaComItens(int id)
    {
        var venda = await _vendaRepository.GetByIdAsync(id);
        if (venda == null) throw new NotFoundException("Venda não encontrada");
        return _mapper.Map<DtoResponseVendaComItens>(venda);
    }

    public async Task<DtoResponseVenda> Create(DtoCriarVenda dto)
    {
        if (dto is null) throw new BadRequestException("Venda inválida!");
        var venda = _mapper.Map<Venda>(dto);
        await _vendaRepository.AddAsync(venda);
        await _vendaRepository.SaveAsync();
        return _mapper.Map<DtoResponseVenda>(venda);
    }

    public async Task CancelarVenda(int vendaId)
    {
        var venda = await _vendaRepository.GetByIdAsync(vendaId);
        if (venda == null) throw new NotFoundException("Venda não encontrada");
        venda.Cancelar();
        await _vendaRepository.SaveAsync();
    }

    public async Task FinalizarVenda(int vendaId)
    {
        var venda = await _vendaRepository.GetByIdAsync(vendaId);
        if (venda == null) throw new NotFoundException("Venda não encontrada");
        if (!venda.ValidarVenda()) throw new BadRequestException("Venda já finalizada ou cancelada.");
        decimal cont = 0;

        foreach (var item in venda.Itens)
        {
            if (item.ValidarItemVenda())
            {
                var exemplar = await _exemplarRepository.GetByIdAsync(item.ExemplarId);
                if (exemplar == null) throw new NotFoundException("Exemplar não encontrado");
                exemplar.Vender();
                var preco = item.DefinirPreco(exemplar.Preco);
                cont += preco;
                item.Vender();
            }
        }
        venda.DefinirPrecoTotal(cont);
        venda.Finalizar();
        await _vendaRepository.SaveAsync();
    }

    public async Task AdicionarItem(int vendaId, int exemplarId)
    {
        var venda = await _vendaRepository.GetByIdAsync(vendaId);
        if (venda == null) throw new NotFoundException("Venda não encontrada");
        var exemplar = await _exemplarRepository.GetByIdAsync(exemplarId);
        if (exemplar == null) throw new NotFoundException("Exemplar não encontrado");
        var item = venda.AdicionarItem(exemplar);
        item.DefinirPreco(exemplar.Preco);
        await _vendaRepository.SaveAsync();
    }

    public async Task ExcluirItem(int vendaId, int itemId)
    {
        var venda = await _vendaRepository.GetByIdAsync(vendaId);
        if (venda == null) throw new NotFoundException("Venda não encontrada");
        venda.ExcluirItem(itemId);
        await _vendaRepository.SaveAsync();
    }   
}
