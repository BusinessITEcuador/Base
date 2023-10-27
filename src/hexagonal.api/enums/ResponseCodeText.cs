using System.ComponentModel;

namespace hexagonal.api.enums
{
    public enum ResponseCodeText
    {
        [Description("OK")]
        OK,

        [Description("SUCCESS")]
        SUCCESS,

        [Description("NO_DATA")]
        NO_DATA,

        [Description("GENERIC_ERROR")]
        GENERIC_ERROR,

        [Description("NOT_SECTION")]
        NOT_SECTION,

        [Description("BAD_REQUEST")]
        BAD_REQUEST,

        [Description("NOT_FOUND")]
        NOT_FOUND,

        [Description("REDIRECT")]
        REDIRECT
    }
}