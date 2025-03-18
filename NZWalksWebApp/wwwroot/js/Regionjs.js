document.addEventListener("DOMContentLoaded", (e) => {
    e.preventDefault();
    document.getElementById("btnAddRegion")?.addEventListener("click", async (e) => {
        try {
            e.preventDefault();
            const reqData = {
                Name: document.getElementById('txtName').value?.trim(),
                Code: document.getElementById('txtCode').value?.trim(),
                RegionImageUrl: document.getElementById('txtUrl').value?.trim()
            };
            const response = await fetch('/Regions/AddRegionUsingAPI', {
                method: 'Post',
                headers: { 'content-type': 'application/json' },
                body: JSON.stringify(reqData)
            });

            if (!response.ok) throw new Error(`HTTP error! Status: ${response.status}`);

            const responseData = await response.json();
            if (responseData.isSuccess) {
                toastr.success("Region added successfully!", "Success");
                document.querySelectorAll('input[type="text"]').forEach(input => input.value = '');
            } else {
                toastr.error("Region not Added!", "Error");
            }
            var data = responseData;
        } catch (ex) {
            console.error(ex);
        }

    });

    document.getElementById("btnUpdateRegion")?.addEventListener("click", async (e) => {
        try {
            e.preventDefault();
            const requestData = {
                Id: document.getElementById('txtId').value?.trim(),
                Name: document.getElementById('txtName').value?.trim(),
                Code: document.getElementById('txtCode').value?.trim(),
                RegionImageUrl: document.getElementById('txtUrl').value?.trim()
            };
            const response = await fetch('/Regions/UpdateRegionUsingAPI', {
                method: 'Post',
                headers: { 'content-type': 'application/json' },
                body: JSON.stringify(requestData)
            });

            if (!response.ok) throw new Error(`HTTP error! Status: ${response.status}`);

            const responseData = await response.json();
            if (responseData?.isSuccess) {
                toastr.success("Region updated successfully!", "Success");
            } else {
                toastr.error("Region not updated!", "Error");
            }
        } catch (ex) {
            console.error(ex);
        }
    });

})