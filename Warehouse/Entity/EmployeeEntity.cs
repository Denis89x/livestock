namespace Warehouse.Entity
{
    internal class EmployeeEntity
    {
        public long employeeId { get; set; }

        public string surname { get; set; }

        public string firstName { get; set; }

        public string patronymic { get; set; }

        public string position {  get; set; }

        public EmployeeEntity() { }

        public EmployeeEntity(long employeeId, string surname, string firstName, string patronymic, string position)
        {
            this.employeeId = employeeId;
            this.surname = surname;
            this.firstName = firstName;
            this.patronymic = patronymic;
            this.position = position;
        }

        public EmployeeEntity(string surname, string firstName, string patronymic, string position)
        {
            this.surname = surname;
            this.firstName = firstName;
            this.patronymic = patronymic;
            this.position = position;
        }
    }
}