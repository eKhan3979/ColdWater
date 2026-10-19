Drop Procedure If Exists u258112148_1.SpKTreino_Gravar;

Delimiter $$

Create Procedure u258112148.SpKTreino_Gravar
(
	In IdTreinoGr		Int,
	In Yyyy_Mm_DdGr		Char(10),
	In Hh_MmGr			Char(5),
	In TotalGr			Int,
	In SegundosGastosGr	Int,
	In IdAmbienteGr		Int,
	In TempAmbienteGr	Int,
	In TempAguaGr		Int
)

Begin

	Declare Id	Int;

	If IfNull(IdTreinoGr, 0) = 0 Then
		Insert Into u258112148_1.TbKTreino
			(Yyyy_Mm_Dd,
			 Hh_Mm,
			 Total,
			 SegundosGastos,
			 IdAmbiente,
			 TempAmbiente,
			 TempAgua)
		Values
			(Yyyy_Mm_DdGr,
			 Hh_MmGr,
			 TotalGr,
			 SegundosGastosGr,
			 IdAmbienteGr,
			 TempAmbienteGr,
			 TempAguaGr);

		Set	Id = Last_Insert_ID();
	Else
		UpDate	u258112148_1.TbKTreino
		Set		Hh_Mm			=	Hh_MmGr,
				Total			=	TotalGr,
				SegundosGastos	=	SegundosGastosGr,
				IdAmbiente		=	IdAmbienteGr,
				TempAmbiente	=	TempAmbienteGr,
				TempAgua		=	TempAguaGr 
		Where	IdTreino		=	IdTreinoGr;

		Set	Id	=	IdTreinoGr;
	End If;

	Select	Id	IdTreino;

End $$

Delimiter ;