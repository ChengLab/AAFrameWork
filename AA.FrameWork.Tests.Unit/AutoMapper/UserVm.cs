using System;
using System.Collections.Generic;
using System.Text;

namespace AA.FrameWork.Tests.Unit.AutoMapper
{
    //viewModel
    public class UserVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
    }

    //dto
    public class UserInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
    }

}
