CREATE PROC FI_SP_ConsBeneficiario
    @ID BIGINT,
    @IDCLIENTE BIGINT = 0
AS
BEGIN
    IF(ISNULL(@ID,-1) = -1)
    BEGIN
        SELECT NOME, CPF, IDCLIENTE, ID 
        FROM BENEFICIARIOS WITH(NOLOCK)
        WHERE (@IDCLIENTE = 0 OR IDCLIENTE = @IDCLIENTE);
    END
    ELSE
    BEGIN
        SELECT NOME, CPF, IDCLIENTE, ID 
        FROM BENEFICIARIOS WITH(NOLOCK)
        WHERE ID = @ID AND (@IDCLIENTE = 0 OR IDCLIENTE = @IDCLIENTE);
    END
END
