using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.CrossCutting.Utils;
using EduConnect.Infra.Data.Context;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class ContaRepository(EduContext context) : IContaRepository
{
    private readonly EduContext _context = context;
    private static bool VerifyDate(DateTime dataLogin)
    {
        var currentDate = DateTime.Now;
        return dataLogin > currentDate;
    }

    public async Task<Result<(bool, int)>> VerifyLogin(string registro, string senha, int maxTentativas)
    {
        var conta = await _context.Contas.FirstOrDefaultAsync(c => c.Registro == registro);
        if (conta == null)
            return Result.Fail("Não foi localizado a conta!");

        if (conta.DataLogin != null)
        {
            var isBlocked = VerifyDate(conta.DataLogin.Value);
            if (isBlocked)
            {
                return Result.Fail("Conta bloqueada!");
            }
            else
            {
                conta.LimiteLogin = 0;
                conta.DataLogin = null;
                _context.Contas.Update(conta);
                await _context.SaveChangesAsync();
            }
        }

        var verify = SegurancaManager.VerificarHash(senha, conta.Senha);
        if (verify)
        {
            return (true, 0);
        }
        else
        {
            conta.LimiteLogin += 1;
            Console.WriteLine(conta.LimiteLogin);
            if (conta.LimiteLogin >= maxTentativas)
            {
                conta.DataLogin = DateTime.Now.AddMinutes(60);
            }
            _context.Contas.Update(conta);
            await _context.SaveChangesAsync();
            return (false, conta.LimiteLogin);
        }
    }

    public async Task<Result<Conta>> GetConta(string registro)
    {

        var conta = await _context.Contas.FirstOrDefaultAsync(c => c.Registro == registro);
        if (conta == null)
            return Result.Fail("Conta não encontrada.");

        return conta;
    }

    public async Task<Result<bool>> EmailExistsAsync(string registro)
    {
        return await _context.Contas.AnyAsync(c => c.Registro == registro);
    }

    public async Task<Result<(string nome, string foto)>> GetInfos(string cargo, string registro)
    {
        switch(cargo) 
        {
            case "Aluno":
                var aluno = await _context.Alunos
                    .FirstOrDefaultAsync(a => a.Registro == registro);
                if (aluno != null)
                {
                    return (aluno.Nome, aluno.Foto);
                }
                break;
            case "Professor":
                var professor = await _context.Professores
                    .FirstOrDefaultAsync(p => p.Registro == registro);
                if (professor != null)
                {                     
                    return (professor.Nome, professor.Foto);
                }
                break;
            default:
                var funcionario = await _context.Funcionarios
                    .FirstOrDefaultAsync(f => f.Registro == registro);
                if (funcionario != null)
                {
                    return (funcionario.Nome, funcionario.Foto);
                }
                break;
        }
        return Result.Fail("Não foi encontrado o usuário");
    }

    public async Task<Result<bool>> ChancePassword(string registro, string senhaNova)
    {
        var conta = await GetConta(registro);
        if (conta.IsFailed)
            return Result.Fail("Conta não encontrada.");
        
        conta.Value.Senha = senhaNova;
        _context.Contas.Update(conta.Value);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Result<bool>> AddContaAsync(Conta contaAdd)
    {
        var conta = await GetConta(contaAdd.Registro);
        if (conta.IsFailed)
            return Result.Fail("Conta não encontrada.");

        await _context.Contas.AddAsync(contaAdd);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Result<bool>> DeleteContaAsync(int id)
    {
        var conta = await _context.Contas.FindAsync(id);
        if (conta == null)
            return Result.Fail("Conta não encontrada.");

        conta.Deletado = true;
        _context.Contas.Update(conta);
        await _context.SaveChangesAsync();

        return true;
    }
}
