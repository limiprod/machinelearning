using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

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

    public static void Main(string[] args)
    {
        Console.Title = "Neuros - 100 Exemplos Mutantes";
        NeurosAutoCompiladora neuros = new NeurosAutoCompiladora();

        string[] exemplos = new string[]
        {
            // --- 50 Originais ---
            "public class Ex1 { public static void Main ( ) { int idade = 18; if ( idade >= 18 ) { Console.WriteLine ( \"Maior de idade\" ); } } }",
            "public class Ex2 { public static void Main ( ) { for ( int i = 0; i < 5; i++ ) { Console.WriteLine ( i ); } } }",
            "public class Ex3 { public static void Main ( ) { string[] nomes = { \"Rafa\", \"Ana\", \"Joao\" }; Console.WriteLine ( nomes[0] ); } }",
            "public class Ex4 { public static int Somar ( int a, int b ) { return a + b; } public static void Main ( ) { int r = Somar ( 10, 20 ); } }",
            "using System.Windows.Forms; public class Ex5 { public static void Main ( ) { Form f = new Form ( ); f.Text = \"Janela Epica\"; f.ShowDialog ( ); } }",
            "using System.Windows.Forms; public class Ex6 { public static void Main ( ) { Button b = new Button ( ); b.Text = \"Clica-me\"; MessageBox.Show ( \"Ola Mundo\" ); } }",
            "public class Ex7 { public static void Main ( ) { int x = 0; while ( x < 3 ) { Console.WriteLine ( x ); x++; } } }",
            "public class Ex8 { public static void Main ( ) { int dia = 1; switch ( dia ) { case 1: Console.WriteLine ( \"Segunda\" ); break; } } }",
            "public class Ex9 { public static void Main ( ) { double raiz = Math.Sqrt ( 25 ); Console.WriteLine ( raiz ); } }",
            "public class Ex10 { public static void Main ( ) { try { int a = 10 / 0; } catch ( Exception e ) { Console.WriteLine ( \"Erro detetado\" ); } } }",
            "using System.Windows.Forms; public class Ex11 { public static void Main ( ) { Label l = new Label ( ); l.Text = \"SuperRafaAI Ativa\"; } }",
            "using System.Collections.Generic; public class Ex12 { public static void Main ( ) { List<string> lista = new List<string> ( ); lista.Add ( \"Teste\" ); } }",
            "public class Ex13 { public static void Main ( ) { bool ativo = true; if ( ativo == true ) { Console.WriteLine ( \"Ligado\" ); } } }",
            "public class Ex14 { public static void Main ( ) { string nome = \"Rafa\"; string msg = \"Ola \" + nome; Console.WriteLine ( msg ); } }",
            "using System.Windows.Forms; public class Ex15 { public static void Main ( ) { MessageBox.Show ( \"Aviso importante da IA\", \"Atencao\" ); } }",
            "public class Ex16 { public static void Mostrar ( ) { Console.WriteLine ( \"Metodo executado\" ); } public static void Main ( ) { Mostrar ( ); } }",
            "public class Ex17 { public static void Main ( ) { int x = 10; string res = x > 5 ? \"Maior\" : \"Menor\"; } }",
            "public class Ex18 { public static void Main ( ) { int[,] matriz = new int[2, 2]; matriz[0, 0] = 5; } }",
            "public class Ex19 { public static void Main ( ) { DateTime agora = DateTime.Now; Console.WriteLine ( agora ); } }",
            "using System.Windows.Forms; public class Ex20 { public static void Main ( ) { Form f = new Form ( ); Button b = new Button ( ); b.Text = \"Gerado pela IA\"; f.Controls.Add ( b ); f.ShowDialog ( ); } }",
            "public class Ex21 { public static void Main ( ) { int num = 7; if ( num % 2 == 0 ) { Console.WriteLine ( \"Par\" ); } else { Console.WriteLine ( \"Impar\" ); } } }",
            "public class Ex22 { public static void Main ( ) { Random rnd = new Random ( ); int sorteio = rnd.Next ( 1, 10 ); Console.WriteLine ( sorteio ); } }",
            "public class Ex23 { public static void Main ( ) { string texto = \"SuperRafa\"; string maiusculas = texto.ToUpper ( ); Console.WriteLine ( maiusculas ); } }",
            "public class Ex24 { public static void Main ( ) { int[] numeros = { 10, 20, 30, 40 }; int soma = 0; foreach ( int n in numeros ) { soma += n; } } }",
            "using System.IO; public class Ex25 { public static void Main ( ) { File.WriteAllText ( \"teste.txt\", \"Ola Ficheiro\" ); } }",
            "using System.Windows.Forms; public class Ex26 { public static void Main ( ) { TextBox t = new TextBox ( ); t.Text = \"Escreve aqui\"; } }",
            "public class Ex27 { public static void Saudacao ( string nome ) { Console.WriteLine ( \"Ola \" + nome ); } public static void Main ( ) { Saudacao ( \"Rafa\" ); } }",
            "public class Ex28 { public static void Main ( ) { double pi = Math.PI; double potencia = Math.Pow ( 2, 3 ); } }",
            "public class Ex29 { public static void Main ( ) { Environment.Exit ( 0 ); } }",
            "using System.Windows.Forms; public class Ex30 { public static void Main ( ) { Form f = new Form ( ); f.Width = 400; f.Height = 300; f.ShowDialog ( ); } }",
            "using System.Windows.Forms; public class Ex31 { public static void Main ( ) { MessageBox.Show ( \"Bem-vindo ao sistema\" ); } }",
            "public class Ex32 { public static void Main ( ) { string frase = \"C# e poderoso\"; bool contem = frase.Contains ( \"poderoso\" ); Console.WriteLine ( contem ); } }",
            "public class Ex33 { public static void Main ( ) { int a = 5; int b = 12; int max = Math.Max ( a, b ); Console.WriteLine ( max ); } }",
            "using System.Collections.Generic; public class Ex34 { public static void Main ( ) { List<int> numeros = new List<int> ( ); numeros.Add ( 100 ); } }",
            "using System.Windows.Forms; public class Ex35 { public static void Main ( ) { Form f = new Form ( ); f.BackColor = System.Drawing.Color.Red; f.ShowDialog ( ); } }",
            "public class Ex36 { public static void Main ( ) { for ( int i = 10; i > 0; i-- ) { Console.WriteLine ( i ); } } }",
            "public class Ex37 { public static void Main ( ) { string s = \"   Ola   \"; string limpo = s.Trim ( ); Console.WriteLine ( limpo ); } }",
            "public class Ex38 { public static void Main ( ) { char letra = 'A'; int codigo = (int)letra; Console.WriteLine ( codigo ); } }",
            "using System.IO; public class Ex39 { public static void Main ( ) { if ( File.Exists ( \"teste.txt\" ) ) { Console.WriteLine ( \"Existe\" ); } } }",
            "using System.Windows.Forms; public class Ex40 { public static void Main ( ) { Button b = new Button ( ); b.Location = new System.Drawing.Point ( 50, 50 ); } }",
            "public class Ex41 { public static void Main ( ) { decimal preco = 99.99m; Console.WriteLine ( preco ); } }",
            "public class Ex42 { public static void Main ( ) { Guid id = Guid.NewGuid ( ); Console.WriteLine ( id ); } }",
            "using System.Windows.Forms; public class Ex43 { public static void Main ( ) { Label l = new Label ( ); l.AutoSize = true; l.Text = \"Texto dinamico\"; } }",
            "public class Ex44 { public static void Main ( ) { int[] nums = { 5, 2, 9, 1 }; Array.Sort ( nums ); } }",
            "public class Ex45 { public static void Main ( ) { string email = \"teste@email.com\"; bool valido = email.Contains ( \"@\" ); } }",
            "public class Ex46 { public static void Main ( ) { TimeSpan espera = TimeSpan.FromSeconds ( 5 ); System.Threading.Thread.Sleep ( 100 ); } }",
            "using System.Windows.Forms; public class Ex47 { public static void Main ( ) { Form f = new Form ( ); f.MaximizeBox = false; f.ShowDialog ( ); } }",
            "public class Ex48 { public static void Main ( ) { double absoluto = Math.Abs ( -15.5 ); Console.WriteLine ( absoluto ); } }",
            "public class Ex49 { public static void Main ( ) { string val = \"123\"; int convertido = int.Parse ( val ); Console.WriteLine ( convertido ); } }",
            "using System.Windows.Forms; public class Ex50 { public static void Main ( ) { Form f = new Form ( ); Button b = new Button ( ); b.Text = \"Sair\"; f.Controls.Add ( b ); f.ShowDialog ( ); } }",

            // --- 50 Novos Exemplos (Ex51 a Ex100) ---
            "public class Ex51 { public static void Main ( ) { long numeroLongo = 987654321L; Console.WriteLine ( numeroLongo ); } }",
            "public class Ex52 { public static void Main ( ) { float fVal = 5.5f; float resultado = fVal * 2; Console.WriteLine ( resultado ); } }",
            "public class Ex53 { public static void Main ( ) { string txt = \"C Sharp\"; bool inicio = txt.StartsWith ( \"C\" ); Console.WriteLine ( inicio ); } }",
            "public class Ex54 { public static void Main ( ) { string txt = \"Programacao\"; bool fim = txt.EndsWith ( \"ao\" ); Console.WriteLine ( fim ); } }",
            "public class Ex55 { public static void Main ( ) { string txt = \"Testando\"; int idx = txt.IndexOf ( \"t\" ); Console.WriteLine ( idx ); } }",
            "public class Ex56 { public static void Main ( ) { string txt = \"Informatica\"; string sub = txt.Substring ( 0, 4 ); Console.WriteLine ( sub ); } }",
            "public class Ex57 { public static void Main ( ) { string txt = \"computador\"; string minusculas = txt.ToLower ( ); Console.WriteLine ( minusculas ); } }",
            "public class Ex58 { public static void Main ( ) { string txt = \"Ola Mundo\"; string replaced = txt.Replace ( \"Mundo\", \"Rafa\" ); Console.WriteLine ( replaced ); } }",
            "public class Ex59 { public static void Main ( ) { string uniao = string.Concat ( \"Super\", \"Rafa\" ); Console.WriteLine ( uniao ); } }",
            "public class Ex60 { public static void Main ( ) { bool check = string.IsNullOrEmpty ( \"\" ); Console.WriteLine ( check ); } }",
            "public class Ex61 { public static void Main ( ) { int x = 5; int y = 10; int min = Math.Min ( x, y ); Console.WriteLine ( min ); } }",
            "public class Ex62 { public static void Main ( ) { double arredondado = Math.Round ( 4.7 ); Console.WriteLine ( arredondado ); } }",
            "public class Ex63 { public static void Main ( ) { double teto = Math.Ceiling ( 4.1 ); Console.WriteLine ( teto ); } }",
            "public class Ex64 { public static void Main ( ) { double piso = Math.Floor ( 4.9 ); Console.WriteLine ( piso ); } }",
            "public class Ex65 { public static void Main ( ) { int angulo = 90; double radianos = angulo * ( Math.PI / 180 ); } }",
            "using System.Collections.Generic; public class Ex66 { public static void Main ( ) { Queue<string> fila = new Queue<string> ( ); fila.Enqueue ( \"Primeiro\" ); } }",
            "using System.Collections.Generic; public class Ex67 { public static void Main ( ) { Stack<int> pilha = new Stack<int> ( ); pilha.Push ( 10 ); } }",
            "using System.Collections.Generic; public class Ex68 { public static void Main ( ) { Dictionary<string, int> dict = new Dictionary<string, int> ( ); dict.Add ( \"Idade\", 25 ); } }",
            "using System.Collections.Generic; public class Ex69 { public static void Main ( ) { HashSet<int> set = new HashSet<int> ( ); set.Add ( 1 ); } }",
            "using System.IO; public class Ex70 { public static void Main ( ) { string diretorio = AppDomain.CurrentDomain.BaseDirectory; } }",
            "using System.IO; public class Ex71 { public static void Main ( ) { Directory.CreateDirectory ( \"PastaTeste\" ); } }",
            "using System.IO; public class Ex72 { public static void Main ( ) { string[] linhas = { \"Linha 1\", \"Linha 2\" }; File.WriteAllLines ( \"arq.txt\", linhas ); } }",
            "using System.IO; public class Ex73 { public static void Main ( ) { FileInfo fi = new FileInfo ( \"teste.cs\" ); long tamanho = fi.Length; } }",
            "public class Ex74 { public static void Main ( ) { DayOfWeek hoje = DateTime.Today.DayOfWeek; Console.WriteLine ( hoje ); } }",
            "public class Ex75 { public static void Main ( ) { DateTime futuro = DateTime.Now.AddDays ( 7 ); Console.WriteLine ( futuro ); } }",
            "public class Ex76 { public static void Main ( ) { TimeSpan span = new TimeSpan ( 2, 0, 0 ); Console.WriteLine ( span.TotalMinutes ); } }",
            "public class Ex77 { public static void Main ( ) { int hex = 0xFF; Console.WriteLine ( hex ); } }",
            "public class Ex78 { public static void Main ( ) { int bin = 0b1010; Console.WriteLine ( bin ); } }",
            "public class Ex79 { public static void Main ( ) { int? nuloSeguro = null; int valorReal = nuloSeguro ?? 10; } }",
            "public class Ex80 { public static void Main ( ) { var nomeInferencia = \"Rafa\"; Console.WriteLine ( nomeInferencia ); } }",
            "public class Ex81 { public static void Main ( ) { var listaInferencia = new List<int> { 1, 2, 3 }; } }",
            "public class Ex82 { public static void Main ( ) { int a = 5; int b = 3; int res = a ^ b; Console.WriteLine ( res ); } }",
            "public class Ex83 { public static void Main ( ) { int deslocado = 4 << 1; Console.WriteLine ( deslocado ); } }",
            "public class Ex84 { public static void Main ( ) { bool cond = true ? true : false; Console.WriteLine ( cond ); } }",
            "public class Ex85 { public static void Main ( ) { int[] arr = new int[5]; Array.Clear ( arr, 0, arr.Length ); } }",
            "public class Ex86 { public static void Main ( ) { int[] arr1 = { 1, 2 }; int[] arr2 = new int[2]; arr1.CopyTo ( arr2, 0 ); } }",
            "public class Ex87 { public static void Main ( ) { string combined = string.Join ( \",\", \"A\", \"B\", \"C\" ); } }",
            "public class Ex88 { public static void Main ( ) { char[] letras = { 'R', 'a', 'f', 'a' }; string s = new string ( letras ); } }",
            "public class Ex89 { public static void Main ( ) { bool letraAlfa = char.IsLetter ( 'A' ); Console.WriteLine ( letraAlfa ); } }",
            "public class Ex90 { public static void Main ( ) { bool numDigito = char.IsDigit ( '5' ); Console.WriteLine ( numDigito ); } }",
            "using System.Windows.Forms; public class Ex91 { public static void Main ( ) { Form f = new Form ( ); f.StartPosition = FormStartPosition.CenterScreen; } }",
            "using System.Windows.Forms; public class Ex92 { public static void Main ( ) { Button b = new Button ( ); b.Enabled = true; b.Width = 100; } }",
            "using System.Windows.Forms; public class Ex93 { public static void Main ( ) { Label l = new Label ( ); l.ForeColor = System.Drawing.Color.Blue; } }",
            "using System.Windows.Forms; public class Ex94 { public static void Main ( ) { TextBox t = new TextBox ( ); t.PasswordChar = '*'; } }",
            "using System.Windows.Forms; public class Ex95 { public static void Main ( ) { CheckBox cb = new CheckBox ( ); cb.Checked = true; } }",
            "using System.Windows.Forms; public class Ex96 { public static void Main ( ) { RadioButton rb = new RadioButton ( ); rb.Text = \"Opcao\"; } }",
            "using System.Windows.Forms; public class Ex97 { public static void Main ( ) { ProgressBar pb = new ProgressBar ( ); pb.Value = 50; } }",
            "using System.Windows.Forms; public class Ex98 { public static void Main ( ) { ComboBox cb = new ComboBox ( ); cb.Items.Add ( \"Item 1\" ); } }",
            "using System.Windows.Forms; public class Ex99 { public static void Main ( ) { ListBox lb = new ListBox ( ); lb.Items.Add ( \"Dado\" ); } }",
            "using System.Windows.Forms; public class Ex100 { public static void Main ( ) { Panel p = new Panel ( ); p.BorderStyle = BorderStyle.FixedSingle; } }"
        };

        foreach (string ex in exemplos) neuros.Aprender(ex);

        Console.WriteLine("[Neuros] 100 Exemplos carregados! Pressiona Enter para explorar a base expandida...\n");

        while (true)
        {
            Console.ReadKey(true);
            Console.Clear();

            string codigoGerado = "";
            bool compilaComSucesso = false;
            int tentativas = 0;

            while (!compilaComSucesso && tentativas < 10)
            {
                tentativas++;
                codigoGerado = neuros.GerarCodigoMutado();
                compilaComSucesso = neuros.TestarSeCompila(codigoGerado);

                if (!compilaComSucesso)
                {
                    neuros.RegistarErro(codigoGerado);
                }
            }

            Console.WriteLine("=== NEUROS (Tentativa: " + tentativas + " de 10) ===");
            Console.WriteLine(codigoGerado);
            Console.WriteLine("\n=================================================");

            if (compilaComSucesso)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[STATUS] Sucesso! Código gerado e verificado instantaneamente.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[STATUS] A ajustar mutação.");
                Console.ResetColor();
            }

            Console.WriteLine("\nPressiona outra tecla para o próximo.");
        }
    }
}