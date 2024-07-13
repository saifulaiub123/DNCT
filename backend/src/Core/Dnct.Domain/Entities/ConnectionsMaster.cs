using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ConnectionsMaster
{
    [Key]
    [Column("contn_id")]
    public int ContnId { get; set; }

    [Column("created_user_name")]
    public string CreatedUserName { get; set; }

    [Column("contn_name")]
    public string ContnName { get; set; }

    [Column("host_ip")]
    public string HostIp { get; set; }

    [Column("user_name")]
    public string UserName { get; set; }

    [Column("paswd")]
    public string Paswd { get; set; }

    [Column("platform_type")]
    public string PlatformType { get; set; }

    [Column("logmech")]
    public string Logmech { get; set; }

    [Column("td_parameter")]
    public string TdParameter { get; set; }

    [Column("default_db")]
    public string DefaultDb { get; set; }

    [Column("accnt_str")]
    public string AccntStr { get; set; }

    [Column("orcl_tns_alias")]
    public string OrclTnsAlias { get; set; }

    [Column("private_key")]
    public string PrivateKey { get; set; }

    [Column("public_key")]
    public string PublicKey { get; set; }

    [Column("encr_key")]
    public string EncrKey { get; set; }

    [Column("enabled_ind")]
    public int? EnabledInd { get; set; }

    [Column("contn_port")]
    public string ContnPort { get; set; }

    [Column("attr1")]
    public string Attr1 { get; set; }

    [Column("attr2")]
    public string Attr2 { get; set; }

    [Column("attr3")]
    public string Attr3 { get; set; }
}