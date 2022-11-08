using System.Text;
using BaseProject.Models;

namespace WebApp.Template.UserCards
{
    public abstract class UserCardTemplate
    {

        protected AppUser _appUser { get; set; }

        public void SetUser(AppUser appUser)
        {
            _appUser = appUser;}

        public string Build()
        {
            if (_appUser==null)

            {
                throw new ArgumentNullException(nameof(_appUser));

            }

            var sb = new StringBuilder();
            sb.Append("<div class='card' >");
            sb.Append(SetPicture());
            sb.Append($@"<div class='card-body'>
             <h5>{_appUser.UserName}</h5>
              <p>{_appUser.Description}</p>");
            sb.Append(SetFooter());
            sb.Append("</div>");

            sb.Append("</div>");

            return sb.ToString();

        }
        protected abstract string SetFooter();
        protected abstract string SetPicture();

    }
}
