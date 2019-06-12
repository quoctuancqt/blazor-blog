window.todo = {
    showControl(id, item, bShow) {
        if (bShow) {
            var txtInput = document.getElementById("txt_" + id);
            document.getElementById("spn_" + id).style.display = "none";
            txtInput.style.display = "";
            txtInput.value = item;
            txtInput.focus();
            document.getElementById("btnEdit_" + id).style.display = "none";
            document.getElementById("btnUpdate_" + id).style.display = "";
            document.getElementById("btnCancel_" + id).style.display = "";
        }
        else {
            document.getElementById("spn_" + id).style.display = "";
            document.getElementById("txt_" + id).style.display = "none";
            document.getElementById("btnEdit_" + id).style.display = "";
            document.getElementById("btnUpdate_" + id).style.display = "none";
            document.getElementById("btnCancel_" + id).style.display = "none";
        }
        return true;
    }
};