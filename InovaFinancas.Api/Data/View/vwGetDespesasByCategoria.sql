Create view vwGetDespesasByCategoria as
SELECT 
	Transacao.UsuarioId,
	Categoria.Titulo as Categoria,
	year(Transacao.DataCriacao) as Ano,
	sum(transacao.Valor) as Despesa
FROM Transacao
	INNER JOIN Categoria ON Transacao.CategoriaId = Categoria.Id
	WHERE Transacao.DataRecebimento >= 	DATEADD(MONTH, -11, CAST(GETDATE() AS DATE))
	AND Transacao.DataRecebimento <= 	DATEADD(MONTH, 1, CAST(GETDATE() AS DATE))
AND Transacao.Tipo = 2
Group by 
	Transacao.UsuarioId, Categoria.Titulo, year(Transacao.DataCriacao)