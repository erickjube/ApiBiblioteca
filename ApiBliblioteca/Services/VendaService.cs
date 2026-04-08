using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.DTOs.DtosVenda;
using ApiBiblioteca.Exceptions;
using ApiBiblioteca.Interfaces;
using AutoMapper;

namespace ApiBiblioteca.Services;

public class VendaService : IVendaService
{
    private readonly IUnitOfWork _UOW;
    private readonly IVendaRepository _vendaRepository;
    private readonly IExemplarRepository _exemplarRepository;
    private readonly IMapper _mapper;

    public VendaService(IVendaRepository vendaRepository, IExemplarRepository exemplarRepository, IMapper mapper, IUnitOfWork uOW)
    {
        _vendaRepository = vendaRepository;
        _exemplarRepository = exemplarRepository;
        _mapper = mapper;
        _UOW = uOW;
    }

    public async Task<IEnumerable<VendaResponseDto>> ObterVendas()
    {
        var vendas = await _vendaRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<VendaResponseDto>>(vendas);
    }

    public async Task<VendaResponseDto> ObterVendaPorId(int id)
    {
        var venda = await _vendaRepository.GetByIdAsync(id);
        if (venda == null) throw new NotFoundException("Venda não encontrada");
        return _mapper.Map<VendaResponseDto>(venda);
    }

    public async Task<VendaComItensDto> ObterVendaComItens(int id)
    {
        var venda = await _vendaRepository.GetByIdAsync(id);
        if (venda == null) throw new NotFoundException("Venda não encontrada");
        return _mapper.Map<VendaComItensDto>(venda);
    }

    public async Task<VendaResponseDto> Create(CreateVendaDto dto)
    {
        if (dto is null) throw new BadRequestException("Venda inválida!");
        var venda = _mapper.Map<Venda>(dto);
        await _vendaRepository.AddAsync(venda);
        await _UOW.SaveAsync();
        return _mapper.Map<VendaResponseDto>(venda);
    }

    public async Task CancelarVenda(int vendaId)
    {
        var venda = await _vendaRepository.GetByIdAsync(vendaId);
        if (venda == null) throw new NotFoundException("Venda não encontrada");

        foreach (var item in venda.Itens)
        {
            item.Cancelar();
        }
        venda.Cancelar();
        await _UOW.SaveAsync();
    }

    public async Task FinalizarVenda(int vendaId)
    {
        var venda = await _vendaRepository.GetByIdAsync(vendaId);
        if (venda == null) throw new NotFoundException("Venda não encontrada");
        if (!venda.ValidarVenda()) throw new BadRequestException("Venda já finalizada ou cancelada.");
        decimal cont = 0;

        foreach (var item in venda.Itens)
        {
            var exemplar = await _exemplarRepository.GetByIdAsync(item.ExemplarId);
            if (exemplar == null) throw new NotFoundException("Exemplar não encontrado");
            exemplar.Vender();
            var preco = item.DefinirPreco(exemplar.Preco);
            cont += preco;
            item.Vender();
        }
        venda.DefinirPrecoTotal(cont);
        venda.Finalizar();
        await _UOW.SaveAsync();
    }

    public async Task AdicionarItem(int vendaId, int exemplarId)
    {
        var venda = await _vendaRepository.GetByIdAsync(vendaId);
        if (venda == null) throw new NotFoundException("Venda não encontrada");
        var exemplar = await _exemplarRepository.GetByIdAsync(exemplarId);
        if (exemplar == null) throw new NotFoundException("Exemplar não encontrado");
        var item = venda.AdicionarItem(exemplar);
        item.DefinirPreco(exemplar.Preco);
        await _UOW.SaveAsync();
    }

    public async Task ExcluirItem(int vendaId, int itemId)
    {
        var venda = await _vendaRepository.GetByIdAsync(vendaId);
        if (venda == null) throw new NotFoundException("Venda não encontrada");
        venda.ExcluirItem(itemId);
        await _UOW.SaveAsync();
    }
}
