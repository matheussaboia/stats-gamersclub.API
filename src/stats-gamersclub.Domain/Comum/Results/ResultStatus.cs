namespace stats_gamersclub.Domain.Comum.Results {
    public enum ResultStatus {
        /// <summary>
        /// 200 OK
        /// Utilizado quando uma operação ou consulta é executada com sucesso.
        /// </summary>
        Ok,

        /// <summary>
        /// 201 Created
        /// Utilizado quando um novo recurso é criado.
        /// </summary>
        Created,

        /// <summary>
        /// 400 Bad Request
        /// Erro no formato da requisição ou nos parametros da consulta.
        /// Ex: Formato do json inválido, parametro obrigatório de uma consulta não informado, etc.
        /// </summary>
        BadRequest,

        /// <summary>
        /// 404 Not Found
        /// Utilizado para retornar um erro de quando um recurso não é encontrado em uma consulta.
        /// </summary>
        NotFound,

        /// <summary>
        /// 422 Unprocessable Entity
        /// Utilizado para retornar um erro de negócio.
        /// </summary>
        UnprocessableEntity,

        /// <summary>
        /// 401 Unauthorized
        /// Utilizado quando o usuário não está autenticado ou a sessão está expirada.
        /// </summary>
        Unauthorized,

        /// <summary>
        /// 403 Forbidden
        /// Utilizado quando o usuário está autenticado, mas não tem permissão para executar uma consulta ou operação.
        /// </summary>
        Forbidden,

        /// <summary>
        /// 500 Internal Server Error
        /// Erro quando ocorre uma falha inesperada no servidor.
        /// Ex: Falha de conexão de rede com recursos externos, eventuais exceções não tratadas, indisponibilidade em serviços de terceiros, etc.
        /// </summary>
        InternalServerError,
    }
}
