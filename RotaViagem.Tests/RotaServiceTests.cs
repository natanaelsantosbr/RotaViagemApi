﻿using RotaViagem.Application;
using RotaViagem.Domain;
using RotaViagem.Infrastructure;

namespace RotaViagem.Tests;

public class RotaServiceTests
{
    private RotaService CriarServicoComDadosDeExemplo()
    {
        var repo = new InMemoryRotaRepository();
        repo.Add(new Rota { Origem = "GRU", Destino = "BRC", Valor = 10 });
        repo.Add(new Rota { Origem = "BRC", Destino = "SCL", Valor = 5 });
        repo.Add(new Rota { Origem = "GRU", Destino = "CDG", Valor = 75 });
        repo.Add(new Rota { Origem = "GRU", Destino = "SCL", Valor = 20 });
        repo.Add(new Rota { Origem = "GRU", Destino = "ORL", Valor = 56 });
        repo.Add(new Rota { Origem = "ORL", Destino = "CDG", Valor = 5 });
        repo.Add(new Rota { Origem = "SCL", Destino = "ORL", Valor = 20 });

        return new RotaService(repo);
    }

    [Fact]
    public void MelhorRota_GRU_para_CDG_DeveSerMaisBarataComConexoes()
    {
        var service = CriarServicoComDadosDeExemplo();
        var (caminho, custo) = service.BuscarMelhorRota("GRU", "CDG");

        Assert.Equal(40, custo);
        Assert.Equal(new[] { "GRU", "BRC", "SCL", "ORL", "CDG" }, caminho);
    }

    [Fact]
    public void MelhorRota_BRC_para_SCL_DeveSerDireta()
    {
        var service = CriarServicoComDadosDeExemplo();
        var (caminho, custo) = service.BuscarMelhorRota("BRC", "SCL");

        Assert.Equal(5, custo);
        Assert.Equal(new[] { "BRC", "SCL" }, caminho);
    }

    [Fact]
    public void MelhorRota_Inexistente_DeveRetornarCustoNegativo()
    {
        var service = CriarServicoComDadosDeExemplo();
        var (caminho, custo) = service.BuscarMelhorRota("GRU", "XYZ");

        Assert.Equal(-1, custo);
        Assert.Empty(caminho);
    }

    [Fact]
    public void MelhorRota_ComOrigemIgualDestino_DeveSerZero()
    {
        var service = CriarServicoComDadosDeExemplo();
        var (caminho, custo) = service.BuscarMelhorRota("GRU", "GRU");

        Assert.Equal(0, custo);
        Assert.Equal(new[] { "GRU" }, caminho);
    }

    [Fact]
    public void MelhorRota_ComMultiplosCaminhos_DeveEscolherMaisBarato()
    {
        var repo = new InMemoryRotaRepository();
        repo.Add(new Rota { Origem = "A", Destino = "B", Valor = 10 });
        repo.Add(new Rota { Origem = "A", Destino = "C", Valor = 2 });
        repo.Add(new Rota { Origem = "C", Destino = "B", Valor = 1 });

        var service = new RotaService(repo);
        var (caminho, custo) = service.BuscarMelhorRota("A", "B");

        Assert.Equal(3, custo);
        Assert.Equal(new[] { "A", "C", "B" }, caminho);
    }

    [Fact]
    public void MelhorRota_ComCiclo_DeveIgnorarLoopECalcularCorretamente()
    {
        var repo = new InMemoryRotaRepository();
        repo.Add(new Rota { Origem = "A", Destino = "B", Valor = 5 });
        repo.Add(new Rota { Origem = "B", Destino = "C", Valor = 5 });
        repo.Add(new Rota { Origem = "C", Destino = "A", Valor = 5 }); // ciclo
        repo.Add(new Rota { Origem = "C", Destino = "D", Valor = 1 });

        var service = new RotaService(repo);
        var (caminho, custo) = service.BuscarMelhorRota("A", "D");

        Assert.Equal(11, custo); // A → B → C → D
        Assert.Equal(new[] { "A", "B", "C", "D" }, caminho);
    }

    [Fact]
    public void MelhorRota_SemRotasCadastradas_DeveRetornarNaoEncontrado()
    {
        var service = new RotaService(new InMemoryRotaRepository());
        var (caminho, custo) = service.BuscarMelhorRota("A", "B");

        Assert.Equal(-1, custo);
        Assert.Empty(caminho);
    }

    [Fact]
    public void MelhorRota_CaseInsensitive_DeveEncontrarRotaMesmoComLetrasMinusculas()
    {
        var service = CriarServicoComDadosDeExemplo();
        var (caminho, custo) = service.BuscarMelhorRota("gru", "cdg");

        Assert.Equal(40, custo);
        Assert.Equal(new[] { "GRU", "BRC", "SCL", "ORL", "CDG" }, caminho);
    }




}
