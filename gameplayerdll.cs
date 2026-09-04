using System;
using System.Collections.Generic;
using System.Threading;

namespace MinecraftVoxelEngine
{
    public class JogadorVirtual
    {
        public string Nome { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Pontos { get; set; }
        public bool IsAI { get; set; }
        public string CorAnsi { get; set; }

        public JogadorVirtual(string nome, int x, int y, string corAnsi, bool isAI)
        {
            Nome = nome;
            X = x;
            Y = y;
            Pontos = 0;
            CorAnsi = corAnsi;
            IsAI = isAI;
        }

        public void JogarAutomaticamente(int objX, int objY, int larguraGrid, int alturaGrid)
        {
            if (X < objX) X++;
            else if (X > objX) X--;

            if (Y < objY) Y++;
            else if (Y > objY) Y--;

            if (X < 1) X = 1; if (X >= larguraGrid - 1) X = larguraGrid - 2;
            if (Y < 1) Y = 1; if (Y >= alturaGrid - 1) Y = alturaGrid - 2;
        }
    }

    // Classe estática com métodos expostos para o Testador de DLLs
    public static class IniciarMinecraft
    {
        private static int largura = 40;
        private static int altura = 18;
        private static int objetivoX;
        private static int objetivoY;
        private static Random rand = new Random();

        private const string COR_CEU = "\x1b[48;2;135;206;235m\x1b[38;2;135;206;235m";
        private const string COR_RELVA = "\x1b[48;2;85;140;40m\x1b[38;2;60;110;30m";
        private const string COR_TERRA = "\x1b[48;2;120;80;40m\x1b[38;2;90;60;30m";
        private const string COR_PEDRA = "\x1b[48;2;110;110;110m\x1b[38;2;80;80;80m";
        private const string COR_OURO = "\x1b[48;2;255;215;0m\x1b[38;2;200;160;0m";
        private const string COR_STEVE = "\x1b[48;2;50;100;220m\x1b[38;2;255;255;255m";
        private const string COR_ZOMBIE = "\x1b[48;2;40;120;60m\x1b[38;2;255;255;255m";
        private const string RESET = "\x1b[0m";

        private static int[,] mapa;

        // Método estático que o teu testador vai encontrar sem erros
        public static void ExecutarJogo()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            GerarTerreno();

            JogadorVirtual voce = new JogadorVirtual("S", 2, altura - 4, COR_STEVE, false);
            JogadorVirtual zombie = new JogadorVirtual("Z", largura - 3, altura - 4, COR_ZOMBIE, true);

            GerarNovoObjetivo();

            bool aJogar = true;
            while (aJogar)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("=== MINECRAFT TERMINAL EDITION (DLL Estática) ===");
                Console.WriteLine("Steve (S): " + voce.Pontos + " pts | Zombie (Z): " + zombie.Pontos + " pts   ");

                for (int y = 0; y < altura; y++)
                {
                    for (int x = 0; x < largura; x++)
                    {
                        if (x == objetivoX && y == objetivoY)
                        {
                            Console.Write(COR_OURO + "O " + RESET);
                        }
                        else if (x == voce.X && y == voce.Y)
                        {
                            Console.Write(voce.CorAnsi + "S " + RESET);
                        }
                        else if (x == zombie.X && y == zombie.Y)
                        {
                            Console.Write(zombie.CorAnsi + "Z " + RESET);
                        }
                        else
                        {
                            int tipoBloco = mapa[x, y];
                            if (tipoBloco == 0) Console.Write(COR_CEU + "  " + RESET);
                            else if (tipoBloco == 1) Console.Write(COR_RELVA + "▓▓" + RESET);
                            else if (tipoBloco == 2) Console.Write(COR_TERRA + "▓▓" + RESET);
                            else Console.Write(COR_PEDRA + "▓▓" + RESET);
                        }
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("Controlos: Setas para mover. Recolhe o ouro (O). [ESC] Sair.");

                zombie.JogarAutomaticamente(objetivoX, objetivoY, largura, altura);
                VerificarColisao(zombie);

                if (Console.KeyAvailable)
                {
                    var tecla = Console.ReadKey(true).Key;
                    if (tecla == ConsoleKey.Escape) aJogar = false;

                    int novoX = voce.X;
                    int novoY = voce.Y;

                    if (tecla == ConsoleKey.UpArrow) novoY--;
                    if (tecla == ConsoleKey.DownArrow) novoY++;
                    if (tecla == ConsoleKey.LeftArrow) novoX--;
                    if (tecla == ConsoleKey.RightArrow) novoX++;

                    if (novoX >= 0 && novoX < largura && novoY >= 0 && novoY < altura)
                    {
                        if (mapa[novoX, novoY] == 0)
                        {
                            voce.X = novoX;
                            voce.Y = novoY;
                        }
                    }

                    VerificarColisao(voce);
                }

                Thread.Sleep(100);
            }
        }

        private static void GerarTerreno()
        {
            mapa = new int[largura, altura];
            for (int x = 0; x < largura; x++)
            {
                int alturaRelva = altura - 5 + (int)(Math.Sin(x * 0.4) * 2);

                for (int y = 0; y < altura; y++)
                {
                    if (y < alturaRelva) mapa[x, y] = 0;
                    else if (y == alturaRelva) mapa[x, y] = 1;
                    else if (y < alturaRelva + 3) mapa[x, y] = 2;
                    else mapa[x, y] = 3;
                }
            }
        }

        private static void GerarNovoObjetivo()
        {
            do
            {
                objetivoX = rand.Next(2, largura - 2);
                objetivoY = rand.Next(2, altura - 5);
            } while (mapa[objetivoX, objetivoY] != 0);
        }

        private static void VerificarColisao(JogadorVirtual j)
        {
            if (j.X == objetivoX && j.Y == objetivoY)
            {
                j.Pontos++;
                GerarNovoObjetivo();
            }
        }
    }
}