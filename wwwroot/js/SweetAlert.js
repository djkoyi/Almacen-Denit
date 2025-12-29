function Alert(type, title, msg, url, dotnetHelper, methodName) {
    switch (type) {
        case "Basic":
            Swal.fire(msg);
            break;
        case "Success":
            Swal.fire(title, msg, "success");
            break;
        case "Error":
            Swal.fire(title, msg, "error");
            break;
        case "Warning":
            Swal.fire(title, msg, "warning");
            break;
        case "Question":
            Swal.fire(title, msg, "question");
            break;
        case "Actions":
            Swal.fire({
                title: title,
                text: msg,
                icon: "warning",
                confirmButtonText: 'OK'
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = url;
                }
            });
            break;
        case "ActionsSuccess":
            Swal.fire({
                title: title,
                text: msg,
                icon: "success",
                confirmButtonText: 'OK'
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = url;
                }
            });
            break;
        case "Confirm":
            Swal.fire({
                title: title,
                text: msg,
                icon: "question",
                showCancelButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No'
            }).then((result) => {
                if (dotnetHelper && methodName) {
                    dotnetHelper.invokeMethodAsync(methodName, result.isConfirmed);
                }
            });
            break;
    }
}
