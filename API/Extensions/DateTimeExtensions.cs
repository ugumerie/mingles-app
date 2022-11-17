namespace API.Extensions;

public static class DateTimeExtensions
{
    public static int GetAge(this DateTime dob) // 13/12/2000
    {
        var today = DateTime.Today;
        var age = today.Year - dob.Year; // 2022 - 2000 => 22

        // Yet to celebrate birthday
        if (dob.Date > today.AddYears(-age))  --age; //21

        return age;
    }
}