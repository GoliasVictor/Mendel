using System;
using Mendel;
void WriteGametas(Genoma genoma){
	foreach (var gameta in genoma.PossiveisGametas())
		Console.WriteLine($" - {gameta}");
}


var CorOlhos = new Caracteristica("Cor dos olhos",'m');
var CorPelo =  new Caracteristica("Pelo", 'b');
var Albinismo = new Caracteristica("Albinismo",'a');
var Nanismo =  new Caracteristica("Nanismo", 'n');
var TamanhoPelos =  new Caracteristica("Tamanho pelos", 'g');


Genoma genoma = new Genoma(
	new ParCromossomo(
		new Cromossomo(1,TamanhoPelos.Dominante),
		new Cromossomo(1,TamanhoPelos.Recessivo)
	),
	new ParCromossomo( 
		new Cromossomo(2,CorOlhos.Dominante,CorPelo.Recessivo),
		new Cromossomo(2,CorOlhos.Recessivo,CorPelo.Dominante)
	),

	new ParCromossomo(
		new Cromossomo(3,Albinismo.Dominante,Nanismo.Dominante),
		new Cromossomo(3,Albinismo.Recessivo,Nanismo.Recessivo)
	)
);




Console.WriteLine("Cromossomo Pai:"+genoma.Pai);
Console.WriteLine("Cromossomo Mae:"+genoma.Mae);
Console.WriteLine("Genotipo:"+genoma);
Console.WriteLine("Fenotipo:"+genoma.Fenotipo);
Console.WriteLine("PossiveisGametas:");
WriteGametas(genoma);



Genoma Pai = new Genoma(
	new ParCromossomo(
		new Cromossomo(1,TamanhoPelos.Dominante),
		new Cromossomo(1,TamanhoPelos.Recessivo)
	),
	new ParCromossomo( 
		new Cromossomo(2,CorOlhos.Recessivo),
		new Cromossomo(2,CorOlhos.Dominante)
	)
);
Genoma Mae = new Genoma(
	new ParCromossomo(
		new Cromossomo(1,TamanhoPelos.Dominante),
		new Cromossomo(1,TamanhoPelos.Recessivo)
	),
	new ParCromossomo( 
		new Cromossomo(2,CorOlhos.Dominante),
		new Cromossomo(2,CorOlhos.Recessivo)
	)
);

Console.WriteLine($"Pai: {Pai}");
Console.WriteLine($"Mae: {Mae}");
Console.WriteLine();
Console.WriteLine("Possiveis Gametas Pai:");
WriteGametas(Pai);
Console.WriteLine("Possiveis Gametas Mae:");
WriteGametas(Mae);
Console.WriteLine($"Possiveis Filhos:");
foreach (var poss in Genoma.PossiveisFilhos(Pai, Mae))
{
	Console.WriteLine($" - {poss.reps:2} {poss.genoma} ");
}