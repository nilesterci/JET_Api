As tabelas do projeto e de criação dos logs são geradas por Migration, utilizando o seguinte código:

Add-Migration NewMigration -Project JET.Infra.Data


update-database

Foi utilizado o banco de dados em SQL Server.
As configurações de acesso ao banco no projeto API estão salvas usando User Secret, funcionalidade das últimas versões do .NET Core.
