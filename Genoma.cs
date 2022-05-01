using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mendel
{

	class Genoma{
		ParCromossomo[] ParesCromossomos;
		public CodGenetico Pai =>new CodGenetico(ParesCromossomos.SelectMany(pc=> pc.Pai).ToArray());
		public CodGenetico Mae =>new CodGenetico(ParesCromossomos.SelectMany(pc=> pc.Mae).ToArray());
		public CodGenetico Fenotipo {get{
			return new CodGenetico(ParesCromossomos.SelectMany(pc=> pc.Fenotipo).ToArray());
		}}
		public Genoma(params ParCromossomo[] paresCromossomos)
		{
			ParesCromossomos = paresCromossomos;
		}
		public Genoma(IEnumerable<ParCromossomo> paresCromossomos)
		{
			ParesCromossomos = paresCromossomos.ToArray();
		}
		public override string ToString()
		{
			return string.Join<ParCromossomo>("",ParesCromossomos);
		}

		public Gameta[] PossiveisGametas(){
			var Lista = new LinkedList<ParCromossomo>(ParesCromossomos);
			if(Lista.First is not null)
				return PossiveisGametas(Lista.First).ToArray();
			return new Gameta[0];
		}
		List<Gameta> PossiveisGametas(LinkedListNode<ParCromossomo> nodeParCromossomo){
			var parCromossomo = nodeParCromossomo.Value;
			
			List<Gameta> Proximos;
			if(nodeParCromossomo.Next is not null)
				Proximos = PossiveisGametas(nodeParCromossomo.Next);
			else 
				Proximos = new List<Gameta>(){new Gameta()};

			var AuxPai = Proximos.Select( gameta => new Gameta(gameta.Append(parCromossomo.Pai)));
			var AuxMae = Proximos.Select( gameta => new Gameta(gameta.Append(parCromossomo.Mae)));
			return AuxMae.Concat(AuxPai).ToList();
		}
		public record class Repeticoes(int qt, Gameta valor);
		public static List<(int reps, Genoma genoma)> PossiveisFilhos(Genoma pai, Genoma mae){

			var GametasPai = pai.PossiveisGametas()
								.GroupBy(g => g.ToString())
								.Select(x => new Repeticoes(x.Count(), x.First()));

			var GametasMae = mae.PossiveisGametas()
								.GroupBy(g => g.ToString())
								.Select(x => new Repeticoes(x.Count(), x.First()));
			var Possibilidades =  new List<(int,Genoma)>();
			foreach (var repGametaMae in GametasMae)
			{
				foreach (var repGametaPai in GametasPai)
				{
					int reps = repGametaMae.qt * repGametaPai.qt;
					Gameta gametaPai = repGametaPai.valor;
					Gameta gametaMae = repGametaMae.valor;
					var Pares = new List<ParCromossomo>();
					foreach (var cromossomoPai in gametaPai.Cromossomos)
					{
						Cromossomo cromossomoMae = gametaMae.GetCromossomo(cromossomoPai.id);
						Pares.Add(new ParCromossomo(cromossomoPai,cromossomoMae));
					}
					Possibilidades.Add((reps,new Genoma(Pares)));
				}
			}
			return Possibilidades;
		}

		public static List<(int reps, CodGenetico genoma)> PossiveisFilhosFenotipo(Genoma pai, Genoma mae){
			var PossiveisFilhosGenotipo =  PossiveisFilhos(pai,mae);
			var PossFilhos = new List<(int, CodGenetico)>();
			var GroposPoss = PossiveisFilhosGenotipo.GroupBy((p)=> p.genoma.Fenotipo.ToString());	
			foreach (var grupo in GroposPoss)
			{
				int Total = grupo.Select(g=> g.reps).Sum();
				PossFilhos.Add((Total,grupo.First().genoma.Fenotipo));
			}
			return PossFilhos;
		}

	}	

	class Gameta : IEnumerable<Cromossomo> {
		public List<Cromossomo> Cromossomos;
		public Cromossomo GetCromossomo(int id){
			return Cromossomos.Find((c)=> c.id == id)!; 
		}
		public Gameta(IEnumerable<Cromossomo> cromossomos)
		{
			Cromossomos = cromossomos.ToList();
		}
		public Gameta()
		{
			Cromossomos =  new List<Cromossomo>();
		}

		public IEnumerator<Cromossomo> GetEnumerator()
		{
			return ((IEnumerable<Cromossomo>)Cromossomos).GetEnumerator();
		}

		public override string ToString()
		{
			return string.Join<Cromossomo>("",Cromossomos);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)Cromossomos).GetEnumerator();
		}
	}
}