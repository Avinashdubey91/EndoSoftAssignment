namespace EndoSoftAssignment.Models
{
    public class User
    {
        public int UserId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public String Mobile { get; set; }
        public String Password { get; set; }
        public DateTime DOB { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
