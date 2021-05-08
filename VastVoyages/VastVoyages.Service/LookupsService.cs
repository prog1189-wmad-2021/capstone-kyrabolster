using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VastVoyages.Model.DTO;
using VastVoyages.Repository;

namespace VastVoyages.Service
{
    public class LookupsService
    {
        private LookupsRepo repo = new LookupsRepo();

        public List<JobAssignmentsLookupsDTO> GetJobAssignments()
        {
            return repo.RetrieveJobAssignments();
        }

        public List<DepartmentLookupsDTO> GetDepartments()
        {
            return repo.RetrieveDepartments();
        }

        public List<SupervisorLookupsDTO> GetSupervisors(int departmentId)
        {
            return repo.RetrieveSupervisors(departmentId);
        }

        public List<SupervisorLookupsDTO> GetHeadSupervisor(int departmentId)
        {
            return repo.RetrieveHeadSupervisor(departmentId);
        }

        public List<SupervisorLookupsDTO> GetCEO()
        {
            return repo.RetrieveCEO();
        }

        public List<EmployeeStatusLookupsDTO> GetEmployeeStatus()
        {
            return repo.RetrieveEmployeeStatus();
        }
    }
}
