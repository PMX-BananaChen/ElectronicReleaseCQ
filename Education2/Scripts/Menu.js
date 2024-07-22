function qiehuan(num) {
    for (var id = 0; id <= 4;id++) {
        if (id == num) {
          
               document.getElementById("qh_con" + id).style.display = "block";
               document.getElementById("mynav" + id).className = "nav_on";
         
           
        }
        else {

            var cc= document.getElementById("mynav" + id)
            if (cc.className == "nav_on2") {
            }
            else {
                document.getElementById("qh_con" + id).style.display = "none";
                document.getElementById("mynav" + id).className = "";
            }
           
        }
    }

  

}

function qh(num) {


    for (var id = 0; id <= 5; id++) {
        if (id == num) {
            document.getElementById("qh_con" + id).style.display = "block";
            document.getElementById("mynav" + id).className = "nav_on";
            //document.cookie = "num";
        }
        else {

            //var cc = document.getElementById("mynav" + id)
            //if (cc.className == "nav_on2") {

                document.getElementById("qh_con" + id).style.display = "none";
                document.getElementById("mynav" + id).className = "";
            //}

             }
        }
}