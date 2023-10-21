using System.ComponentModel.DataAnnotations.Schema;
using Sparks.Api.Features.Interests;
using Sparks.Api.Shared;

namespace Sparks.Api.Features.Users;


public class User : BaseEntity
{
    public string UserName { get; private set; } = string.Empty;
    public string DisplayName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;
    public int? Age { get; private set; }
    public GenderTypes Gender { get; private set; }
    public bool IsAccountActive { get; private set; }
    public int? HeightInCm { get; private set; }
    public StarSignTypes? StarSign { get; private set; }
    public ICollection<UserPhoto> UserPhotos { get; private set; } = new List<UserPhoto>();
    public ICollection<Interest> Interests { get; private set; } = new List<Interest>();
    
    private User(){}
    
    public static User Create(Guid id, string email, string country, int? age, int? height, GenderTypes gender, StarSignTypes? starSign)
    {
        var newUser = new User()
        {
            Id = id,
            Email = email,
            UserName = email,
            Gender = gender,
            StarSign = starSign,
            HeightInCm = height,
            Age = age,
            IsAccountActive = true
        };
        return newUser;
    }

    public void AddPhoto(string photoUrl)
    {
        UserPhotos.Add(new UserPhoto()
        {
            Url = photoUrl
        });
    }
}

public class UserPhoto
{
    public string? Url { get; set; }
    public bool IsPrimary { get; set; }
}

public enum StarSignTypes
{
    Aries = 1,
    Taurus = 2,
    Gemini = 3,
    Cancer = 4,
    Leo = 5,
    Virgo = 6,
    Libra = 7,
    Scorpio = 8,
    Sagittarius = 9,
    Capricorn = 10,
    Aquarius = 11,
    Pisces = 12
}

public enum GenderTypes
{
    Male = 1,
    Female = 2,
    Unknown = 3
}