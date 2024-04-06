
function AddTeacher() {

    var message = document.getElementById("msg");

    var xhr = new XMLHttpRequest();

    var TeacherFname = document.getElementById("TeacherFname").value;
    var TeacherLname = document.getElementById("TeacherLname").value;
    var EmployeeNum = document.getElementById("EmployeeNum").value;
    var HireDate = document.getElementById("HireDate").value;
    var TeacherSalary = document.getElementById("TeacherSalary").value;



    var TeacherData = {
        "TeacherFname": TeacherFname,
        "TeacherLname": TeacherLname,
        "EmployeeNum": EmployeeNum,
        "HireDate": HireDate,
        "TeacherSalary": TeacherSalary
    };



}