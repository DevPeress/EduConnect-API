using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.CrossCutting.Utils;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class ContaRepository(EduContext context) : IContaRepository
{
    private readonly EduContext _context = context;
    private bool VerifyDate(DateTime dataLogin)
    {
        var currentDate = DateTime.Now;
        return dataLogin > currentDate;
    }

    public async Task<(bool, int)> VerifyLogin(string registro, string senha, int maxTentativas)
    {
        var conta = await _context.Contas.FirstOrDefaultAsync(c => c.Registro == registro);
        if (conta != null)
        {
            if (conta.DataLogin != null)
            {
                var isBlocked = VerifyDate(conta.DataLogin.Value);
                if (isBlocked)
                {
                    return (false, -1);
                }
                else
                {
                    conta.LimiteLogin = 0;
                    conta.DataLogin = null;
                    _context.Contas.Update(conta);
                    _context.SaveChanges();
                }
            }

            var verify = SegurancaManager.VerificarHash(senha, conta.Senha);
            if (verify || senha == conta.Senha)
            {
                return (true, 0);
            } 
            else
            {
                conta.LimiteLogin += 1;
                if (conta.LimiteLogin >= maxTentativas)
                {
                    conta.DataLogin = DateTime.Now.AddMinutes(60);
                    _context.Contas.Update(conta);
                    _context.SaveChanges();
                }
                return (false, conta.LimiteLogin);
            }
        }
        return (false, 0);
    }

    public async Task<Conta?> GetConta(string registro)
    {

        var conta = await _context.Contas.FirstOrDefaultAsync(c => c.Registro == registro);
        if (conta != null)
        {
            return conta;
        }

        return null;
    }

    public async Task<bool> EmailExistsAsync(string registro)
    {
        return await _context.Contas.AnyAsync(c => c.Registro == registro);
    }

    public async Task<(string nome, string foto)> GetInfos(string cargo, string registro)
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
        return (string.Empty, string.Empty);
    }

    public async Task<bool> ChancePassword(string registro, string senhaNova)
    {
        var conta = await _context.Contas.FindAsync(registro);
        if (conta != null)
        {
            conta.Senha = senhaNova;
            _context.Contas.Update(conta);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task AddContaAsync(Conta conta)
    {
        await _context.Contas.AddAsync(conta);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteContaAsync(int id)
    {
        var conta = await _context.Contas.FindAsync(id);
        if (conta != null)
        {
            await Task.Run(() =>
            {
                conta.Deletado = true;
                _context.Contas.Update(conta);
                _context.SaveChanges();
            });
        }
    }
}
