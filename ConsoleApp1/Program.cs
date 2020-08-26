using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static List<Mao> ListaMaosPossiveis;
        static Program()
        {
            ListaMaosPossiveis = ListarPossibilidadesMaos();
        }

        static void Main(string[] args)
        {
            var maoPC = ListaMaosPossiveis[new Random().Next(0, ListaMaosPossiveis.Count)];
            Console.WriteLine($"Mao PC:      {maoPC.Nome}");

            MostrarOpcoesMaos(ListaMaosPossiveis);

            var maoDigitada = int.Parse(Console.ReadLine());
            var maoUsuario = ListaMaosPossiveis[maoDigitada - 1];

            Console.WriteLine($"Mao PC:      {maoPC.Nome}");
            Console.WriteLine($"Mao Usuario: {maoUsuario.Nome}");
            Console.WriteLine($"Resultado: {maoPC.ObterResultado(maoUsuario)}");
            Console.ReadLine();
        }

        private static void MostrarOpcoesMaos(List<Mao> listaMaos)
        {
            Console.WriteLine("Informe jogada");
            var item = 1;
            foreach (var mao in listaMaos)
            {
                Console.WriteLine($"{item++} - {mao.Nome}");
            }
        }

        private static List<Mao> ListarPossibilidadesMaos() => new List<Mao>()
        {
            CriarMaoPapel(),
            CriarMaoPedra(),
            CriarMaoLagarto(),
            CriarMaoSpock(),
            CriarMaoTesoura(),
        };

        private static Mao CriarMaoPapel() => new Mao
        {
            Nome = "Papel",
            Possibilidades =
            {
                { "Pedra", "cobre" },
                { "Spock", "desaprova" },
            }
        };

        private static Mao CriarMaoPedra() => new Mao
        {
            Nome = "Pedra",
            Possibilidades =
            {
                { "Lagarto", "esmaga" },
                { "Tesoura", "destrói" },
            }
        };

        private static Mao CriarMaoSpock() => new Mao
        {
            Nome = "Spock",
            Possibilidades =
            {
                { "Tesoura", "quebra" },
                { "Pedra", "vaporiza" },
            }
        };

        private static Mao CriarMaoLagarto() => new Mao
        {
            Nome = "Lagarto",
            Possibilidades =
            {
                { "Papel", "come" },
                { "Spock", "envenena" },
            }
        };

        private static Mao CriarMaoTesoura() => new Mao
        {
            Nome = "Tesoura",
            Possibilidades =
            {
                { "Papel", "corta" },
                { "Lagarto", "decapita" },
            }
        };
    }

    class Mao
    {
        public string Nome { get; set; }
        public Dictionary<string, string> Possibilidades { get; } = new Dictionary<string, string>();
        public string ObterResultado(Mao mao2)
        {
            if (this == mao2 || this.Nome.Equals(mao2.Nome))
            {
                return $"{Nome} empata com {mao2.Nome}";
            }

            string verbo;
            if (Possibilidades.TryGetValue(mao2.Nome, out verbo))
            {
                return $"{Nome} {verbo} {mao2.Nome}";
            }

            if (mao2.Possibilidades.TryGetValue(this.Nome, out verbo))
            {
                return $"{mao2.Nome} {verbo} {Nome}";
            }

            return $"{mao2.Nome} ????????? {Nome}";
        }
    }
}
