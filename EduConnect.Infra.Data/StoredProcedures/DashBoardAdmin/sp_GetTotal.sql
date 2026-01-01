CREATE OR ALTER PROCEDURE sp_GetTotalDashBoard
AS
BEGIN 
    SET NOCOUNT ON;

	SELECT 
		ISNULL((SELECT COUNT(*) FROM Alunos WHERE Deletado = 0), 0) AS TotalAlunos,
		ISNULL((SELECT COUNT(*) FROM Professores WHERE Deletado = 0), 0) AS TotalProfessores,
		ISNULL((SELECT COUNT(*) FROM Turmas WHERE Deletado = 0), 0) AS TotalTurmas,
		ISNULL((
            SELECT 
                SUM(
                    CASE 
                        WHEN Presente = 0 AND Justificada = 0 THEN 0
                        ELSE 1
                    END
                )
            FROM Presencas
        ), 0) AS TotalPresencas
END