
using System.Collections.ObjectModel;

namespace MyToDo.Common.Models
{
    public class DepartmentDto : TreeItem
    {
        public int deptId {  get; set; }

        public string deptName { get; set; }

        public int parentId { get; set; }

        public string ancestors { get; set; }

        public int status { get; set; }

        public DateTime createdTime { get; set; }

        public DateTime? updateTime { get; set; }

        public bool isEmp { get; set; } = false;

        public ObservableCollection<DepartmentDto> children { get; set; }

        public ObservableCollection<DeptEmployee> deptEmployees { get; set; } = [];

    }
}
