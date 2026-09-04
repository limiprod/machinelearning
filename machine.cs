using System;

public class MiniAINativa
{
    public static void Main(string[] args)
    {
        Console.Title = "Rafa Mini-IA Nativa";
        Console.WriteLine("=== A TREINAR A IA DE RAIZ (SEM INSTALAR NADA) ===");

        // 1. Dados de exemplo (Ex: Horas de estudo vs. Nota final)
        double[] horasEstudo = { 1.0, 2.0, 3.0, 4.0, 5.0 };
        double[] notasObtidas = { 12.0, 24.0, 31.0, 43.0, 52.0 }; // Relação aproximada de x * 10

        double declive = 0;
        double intersecao = 0;

        // 2. O "Treino" da IA: calcula a linha de tendência matemática perfeita
        TreinarModelo(horasEstudo, notasObtidas, out declive, out intersecao);

        Console.WriteLine(string.Format("Treino concluído com sucesso!"));
        Console.WriteLine(string.Format("Modelo aprendido: y = {0} * x + {1}\n", declive, intersecao));

        // 3. Testar a IA com um valor novo que ela nunca viu (Ex: Se estudares 7 horas)
        double novoEstudo = 7.0;
        double previsao = (declive * novoEstudo) + intersecao;

        Console.WriteLine(string.Format("Se estudares {0} horas, a IA prevê que vais ter a nota: {1:F2}", novoEstudo, previsao));

        Console.WriteLine("\nPressiona qualquer tecla para sair...");
        Console.ReadKey();
    }

    // Função de Machine Learning estatístico feita por ti em C# puro
    public static void TreinarModelo(double[] x, double[] y, out double m, out double b)
    {
        int n = x.Length;
        double somaX = 0;
        double somaY = 0;
        double somaXY = 0;
        double somaX2 = 0;

        for (int i = 0; i < n; i++)
        {
            somaX += x[i];
            somaY += y[i];
            somaXY += x[i] * y[i];
            somaX2 += x[i] * x[i];
        }

        // Fórmulas de mínimos quadrados para encontrar a melhor relação de aprendizagem
        m = (n * somaXY - somaX * somaY) / (n * somaX2 - somaX * somaX);
        b = (somaY - m * somaX) / n;
    }
}