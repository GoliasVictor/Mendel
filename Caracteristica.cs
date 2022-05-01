namespace Mendel
{
	class Alelo
	{
		public bool Dominante;
		public Caracteristica caracteristica;

		public Alelo(Caracteristica caracteristica, bool dominante)
		{
			Dominante = dominante;
			this.caracteristica = caracteristica;
		}
		public override string ToString()
		{
			return caracteristica.Letra(Dominante).ToString();
		}
	}
	class Caracteristica
	{
		public string Nome;
		public char LetraDominante;
		public char LetraRecessivo;
		public char Letra(bool Dominante)
		{
			return Dominante ? LetraDominante : LetraRecessivo;
		}

		public Caracteristica(string nome, char letraRecessivo, char? letraDominante = null)
		{
			Nome = nome;
			Dominante = new Alelo(this, true);
			Recessivo = new Alelo(this, false);
			LetraRecessivo = letraRecessivo;
			LetraDominante = letraDominante ?? letraRecessivo.ToString().ToUpperInvariant()[0];
		}
		public readonly Alelo Recessivo;
		public readonly Alelo Dominante;
	}
}