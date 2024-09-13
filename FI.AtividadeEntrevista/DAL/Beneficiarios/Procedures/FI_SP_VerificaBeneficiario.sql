CREATE PROCEDURE FI_SP_VerificaBeneficiario
    @CPF           VARCHAR(11),
    @IDCLIENTE	   BIGINT	
AS
BEGIN
    DECLARE @BeneficiarioId INT;

    SET @BeneficiarioId = -1;

    IF EXISTS (
        SELECT 1 
        FROM BENEFICIARIOS WITH(NOLOCK)
        WHERE CPF = @CPF AND IDCLIENTE = @IDCLIENTE
    )
    BEGIN
        SELECT @BeneficiarioId = ID 
        FROM BENEFICIARIOS WITH(NOLOCK)
        WHERE CPF = @CPF AND IDCLIENTE = @IDCLIENTE;
    END

    SELECT @BeneficiarioId AS BeneficiarioId;
END;
