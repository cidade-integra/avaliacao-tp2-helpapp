using StockApp.Domain.Validation;

namespace StockApp.Domain.Entities
{
    public class Project
    {
        #region Atributos

        public int Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        #endregion

        #region Construtor

        public Project(){}

        public Project(string name, string description, DateTime startDate, DateTime endDate)
        {
            Validate(name, description, startDate, endDate);
        }

        #endregion

        #region Validação

        private void Validate(string name, string description, DateTime startDate, DateTime endDate)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), "Project name is required.");
            DomainExceptionValidation.When(startDate > endDate, "Start date must be before end date.");

            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }

        #endregion
    }
}
