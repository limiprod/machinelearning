using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace NeurosEngine
{
    public class NeurosAutoCompiladora
    {
        private List<string> baseDeConhecimento = new List<string>();
        private HashSet<string> codigosInvalidos = new HashSet<string>();
        private Random rand = new Random();
        private CSharpCodeProvider provider = new CSharpCodeProvider();
        private CompilerParameters parametros;

        public NeurosAutoCompiladora()
        {
            parametros = new CompilerParameters();
            parametros.GenerateExecutable = false;
            parametros.GenerateInMemory = true;
            parametros.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            parametros.ReferencedAssemblies.Add("System.Drawing.dll");
        }

        public void Aprender(string codigoFonte)
        {
            if (!baseDeConhecimento.Contains(codigoFonte))
            {
                baseDeConhecimento.Add(codigoFonte);
            }
        }

        public string GerarCodigoMutado()
        {
            if (baseDeConhecimento.Count == 0) return "public class Erro { }";

            string codigoBase = baseDeConhecimento[rand.Next(baseDeConhecimento.Count)];

            if (rand.Next(2) == 0)
            {
                codigoBase = codigoBase.Replace("18", rand.Next(10, 99).ToString());
            }
            else
            {
                codigoBase = codigoBase.Replace("10", rand.Next(1, 50).ToString());
            }

            return codigoBase;
        }

        public bool TestarSeCompila(string codigo)
        {
            if (codigosInvalidos.Contains(codigo)) return false;

            CompilerResults resultados = provider.CompileAssemblyFromSource(parametros, codigo);
            return !resultados.Errors.HasErrors;
        }

        public void RegistarErro(string codigo)
        {
            if (!codigosInvalidos.Contains(codigo))
            {
                codigosInvalidos.Add(codigo);
            }
        }

        public int ObterTotalExemplos()
        {
            return baseDeConhecimento.Count;
        }
    }
}

// --- NOVO NAMESPACE COM MÉTODOS ESTÁTICOS PARA O TESTADOR DE DLLS ---
namespace NeurosBridge
{
    public class NeurosApi
    {
        private static NeurosEngine.NeurosAutoCompiladora instancia = new NeurosEngine.NeurosAutoCompiladora();

        // Método estático 'Aprender' compatível com o testador
        public static void Aprender(string codigoFonte)
        {
            instancia.Aprender(codigoFonte);
        }

        // Método estático 'GerarCodigoMutado' compatível com o testador
        public static string GerarCodigoMutado()
        {
            return instancia.GerarCodigoMutado();
        }

        // Método estático 'TestarSeCompila' compatível com o testador
        public static bool TestarSeCompila(string codigo)
        {
            return instancia.TestarSeCompila(codigo);
        }

        // Método estático 'ObterTotalExemplos' compatível com o testador
        public static int ObterTotalExemplos()
        {
            return instancia.ObterTotalExemplos();
        }
    }
}