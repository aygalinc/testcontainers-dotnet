namespace DotNet.Testcontainers.Configurations
{
  using System;
  using DotNet.Testcontainers.Builders;
  using JetBrains.Annotations;

  /// <inheritdoc cref="TestcontainerDatabaseConfiguration" />
  [PublicAPI]
  public class MsSqlTestcontainerConfiguration : TestcontainerDatabaseConfiguration
  {
    private const string MsSqlImage = "mcr.microsoft.com/mssql/server:2017-CU28-ubuntu-16.04";

    private const int MsSqlPort = 1433;

    /// <summary>
    /// Initializes a new instance of the <see cref="MsSqlTestcontainerConfiguration" /> class.
    /// </summary>
    public MsSqlTestcontainerConfiguration()
      : this(MsSqlImage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MsSqlTestcontainerConfiguration" /> class.
    /// </summary>
    /// <param name="image">The Docker image.</param>
    public MsSqlTestcontainerConfiguration(string image)
      : base(image, MsSqlPort)
    {
      this.Environments["ACCEPT_EULA"] = "Y";
    }

    /// <inheritdoc />
    public override string Database
    {
      get => this.Environments["MSSQL_DATABASE"];
      set => this.Environments["MSSQL_DATABASE"] = value;
    }

    /// <inheritdoc />
    public override string Username
    {
      get => this.Environments["MSSQL_USER"];
      set => this.Environments["MSSQL_USER"] = value;
    }

    /// <inheritdoc />
    public override string Password
    {
      get => this.Environments["MSSQL_PASSWORD"];
      set => this.Environments["MSSQL_PASSWORD"] = value;
    }

    /// <inheritdoc />
    public override IWaitForContainerOS WaitStrategy => Wait.ForUnixContainer()
      .UntilCommandIsCompleted("/opt/mssql-tools/bin/sqlcmd", "-S", $"localhost,{this.DefaultPort}", "-U", this.Username, "-P", this.Password);
  }
}
