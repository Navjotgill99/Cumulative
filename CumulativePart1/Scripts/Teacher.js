
    function AddTeacher() {

        var message = document.getElementById("msg");

        var url = "http://localhost:51990/TeacherData/AddTeacher/";
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

        xhr.open("POST, url, true");
        xhr.responseType = "json";
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                //request is successful
            }
        }
        xhr.send(JSON.stringify(TeacherData));
    }

    function UpdateTeacher(TeacherId) {
        var message = document.getElementById("msg");


        var url = "api/TeacherData/UpdateTeacher/";
        var xhr = new XMLHttpRequest();

        var TeacherFname = document.getElementById("TeacherFname").value;
        var TeacherLname = document.getElementById("TeacherLname").value;
        var EmployeeNum = document.getElementById("EmployeeNum").value;
        var TeacherSalary = document.getElementById("TeacherSalary").value;

        var TeacherData = {
            "TeacherFname": TeacherFname,
            "TeacherLname": TeacherLname,
            "EmployeeNum": EmployeeNum,
            "TeacherSalary": TeacherSalary,
            "TeacherId": TeacherId
        };

        xhr.open("POST", url, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4) {
                if (xhr.status == 200) {
                    window.location.href = "/Teacher/show" + data.TeacherId;
                } else {
                    message.innerHTML = "Update teacher failed. Status: " + xhr.status;
                    }
            }
        }
        xhr.send(JSON.stringify(TeacherId));       
    }