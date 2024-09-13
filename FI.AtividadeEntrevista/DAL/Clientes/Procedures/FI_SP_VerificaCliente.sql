CREATE PROCEDURE FI_SP_VerificaCliente
    @CPF VARCHAR(11)
AS
BEGIN
    DECLARE @ClienteId INT;

    SET @ClienteId = -1;

    IF EXISTS (
        SELECT 1 
        FROM CLIENTES WITH(NOLOCK)
        WHERE CPF = @CPF
    )
    BEGIN
        SELECT @ClienteId = ID 
        FROM CLIENTES WITH(NOLOCK)
        WHERE CPF = @CPF;
    END

    SELECT @ClienteId AS ClienteId;
END;
