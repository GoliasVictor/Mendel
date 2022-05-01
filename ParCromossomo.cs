namespace Mendel
{
	class ParCromossomo {
		public Cromossomo Pai;
		public Cromossomo Mae;

		public ParCromossomo(Cromossomo p, Cromossomo m)
		{
			this.Pai = p;
			this.Mae = m;
		}
		public ParCromossomo(int i, Alelo[] p,Alelo[] m)
		{
			this.Pai = new Cromossomo(i,p);
			this.Mae = new Cromossomo(i,m);
		}

		bool this[Caracteristica carac]{
			get => Pai[carac] || Mae[carac]; 
		}
		Caracteristica[] Caracteristicas => Pai.Caracteristicas.Union(Mae.Caracteristicas).ToArray();
		public CodGenetico Fenotipo {
			get{
				CodGenetico fenotipo =  new CodGenetico();
				foreach(var carac in Caracteristicas)
					fenotipo.Add(carac, Pai[carac] | Mae[carac]);
				return fenotipo;
			}
		}
		public override string ToString()
		{
				string str =  "";
				foreach(var carac in Caracteristicas)
					str += Pai.GetAlelo(carac).ToString() + Mae.GetAlelo(carac).ToString();
				return str;
		}
	}
}