Gerenciamento de Cursos e Matrículas (Avaliação Técnica)
Este projeto implementa o gerenciamento de Cursos, Alunos e Matrículas conforme os requisitos da avaliação técnica, utilizando .NET 8 (C#) no Back-End e React (Vite) no Front-End.

🚀 Status do Projeto e Escolhas Técnicas
Back-End (API): Desenvolvido em .NET 8.

Front-End (Web): Desenvolvido em React com Vite.

Banco de Dados: Para simplificar o processo de execução e deploy, o projeto utiliza o Entity Framework Core In-Memory Database. Isso garante que a aplicação rode imediatamente sem a necessidade de configurar um servidor SQL externo.

Containerização: O Back-End está empacotado em um container Docker para facilitar a execução.

⚙️ Pré-requisitos para Execução Local
Para rodar este projeto, você precisa ter instalado:

Docker Desktop (para executar o container do Back-End).

Node.js e npm/Yarn (para rodar o Front-End React).

SDK do .NET 8 (opcional, se desejar rodar o Back-End fora do Docker).

1. Execução do Back-End (API)
O Back-End está configurado para rodar em um container Docker, escutando na porta 8080.

A. Construir a Imagem
Na raiz do seu repositório, execute o comando para construir a imagem da API.

docker build -t gerenciamento-api -f Gerenciamento-cursos/Dockerfile .

B. Iniciar o Container
Inicie o container, mapeando a porta interna 8080 para a porta externa 8080.

docker run -d -p 8080:8080 --name gerenciamento-api-run gerenciamento-api

URL da API: O Back-End estará acessível em http://localhost:8080/api.

3. Deploy em Produção (Diferencial)
O projeto foi configurado com o objetivo de facilitar o deploy na nuvem, sendo publicado no Render.

URL Pública do Serviço:

Front-End: https://gerenciamento-matriculas.vercel.app/

Back-End (API): https://gerenciamento-de-cursos.onrender.com/api

Nota: A URL base do Axios no Front-End (client-app/src/services/api.js) foi configurada para usar a URL pública (https://gerenciamento-de-cursos.onrender.com/api) para o ambiente de produção.
