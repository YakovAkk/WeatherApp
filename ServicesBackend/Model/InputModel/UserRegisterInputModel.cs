namespace ServicesBackend.Model.InputModel
{
    public class UserRegisterInputModel : UserLoginInputModel
    {
        public string ConfirmPassword { get; set; }
    }
}
