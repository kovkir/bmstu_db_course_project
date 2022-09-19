namespace db_cp.Models
{
    public enum PlayerSortState
    {
        IdAsc,
        IdDesc,

        SurnameAsc,
        SurnameDesc,

        RatingAsc,
        RatingDesc,

        CountryAsc,
        CountryDesc,

        ClubNameAsc,
        ClubNameDesc,

        PriceAsc,
        PriceDesc
    }

    public enum CoachSortState
    {
        IdAsc,
        IdDesc,

        SurnameAsc,
        SurnameDesc,

        CountryAsc,
        CountryDesc,

        WorkExperienceAsc,
        WorkExperienceDesc
    }

    public enum ClubSortState
    {
        IdAsc,
        IdDesc,

        NameAsc,
        NameDesc,

        CountryAsc,
        CountryDesc,

        FoundationDateAsc,
        FoundationDateDesc
    }

    public enum UserSortState
    {
        IdAsc,
        IdDesc,

        LoginAsc,
        LoginDesc,

        PermissionAsc,
        PermissionDesc,

        RatingSquadAsc,
        RatingSquadDesc
    }

    public enum SquadSortState
    {
        IdAsc,
        IdDesc,

        CoachSurnameAsc,
        CoachSurnameDesc,

        NameAsc,
        NameDesc,

        RatingAsc,
        RatingDesc
    }
}
