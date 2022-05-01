using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Mendel
{

	class CodGenetico : IEnumerable<Alelo> {
		public List<Alelo> alelos;

		public CodGenetico( IEnumerable<Alelo> alelos)
		{
			this.alelos = alelos.ToList();
		}
		public CodGenetico(params Alelo[] alelos)
		{
			this.alelos = alelos.ToList();
		}

		public IEnumerator<Alelo> GetEnumerator()
		{
			return ((IEnumerable<Alelo>)alelos).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)alelos).GetEnumerator();
		}
		public override string ToString()
		{
			return string.Join("",alelos);
		}

				public void Add(Caracteristica caracteristica, bool dominante)
		{
			alelos.Add(new Alelo(caracteristica, dominante));
		}
		public void Add(Alelo alelo)
		{
			alelos.Add(alelo);
		}

		public bool ContainsKey(Caracteristica caracteristica)
		{
			return alelos.Any((a) => a.caracteristica == caracteristica);
		}

		public bool Remove(Caracteristica caracteristica)
		{
			return 0 < alelos.RemoveAll((a) => a.caracteristica == caracteristica);
		}


		public bool TryGetValue(Caracteristica caracteristica, [MaybeNullWhen(false)] out bool value)
		{
			Alelo? alelo = alelos.FirstOrDefault((a) => a.caracteristica == caracteristica);
			if(alelo is null){
				value = false;
				return false;
			}
			else{
				value = alelo.Dominante;
				return true;
			}
		}

		public void Clear() => alelos.Clear();

	}
	class Cromossomo : CodGenetico
	{
		public int id;
		public Caracteristica[] Caracteristicas => alelos.Select(a=> a.caracteristica).ToArray();
		public Alelo GetAlelo(Caracteristica caracteristica){
			return alelos.First((a) => a.caracteristica == caracteristica);
		}
		public bool this[Caracteristica caracteristica] {
			get => GetAlelo(caracteristica).Dominante;
			set => GetAlelo(caracteristica).Dominante = value;
		}		
		public Cromossomo(int id, params Alelo[] alelos) : base(alelos)
		{
			this.id = id;
		}
	}
}