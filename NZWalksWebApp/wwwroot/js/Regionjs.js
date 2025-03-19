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

    document.addEventListener("click", async (e) => {
        try {
            if (!e.target.matches('.btnDeleteData')) return;
            const result = await Swal.fire({
                title: "Are you sure want to delete this Region?",
                text: "You won't be able to revert this!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Yes, delete it!"
            });
            if (result.isConfirmed) {
                const deleteId = e.target.dataset?.id;
                const deleteRowId = e.target.dataset?.rowId;
                if (!deleteId || !deleteRowId || deleteId == "" || deleteRowId == "") {
                    console.error('RegionId or RegionRowId not found');
                    return;
                }
                const response = await fetch(`/Regions/DeleteRegionUsingAPI/${deleteId}`, {
                    method: 'delete',
                    headers: { 'content-type': 'application/json' }
                });
                const responseData = await response?.json();
                if (responseData?.isSuccess) {
                    const rowElement = document.querySelector(`[data-row-id="${deleteRowId}"]`);
                    if (rowElement) {
                        const row = rowElement.closest("tr"); // Ensure it's the entire <tr>
                        if (row) {
                            row.remove();
                        }
                    }
                    toastr.success(`<b>${responseData.responseData.name}</b> Region Deleted Successfully!!`, 'Success');
                } else {
                    toastr.error(`RegionId: ${deleteId} Not Deleted ${responseData.message} !!`, 'Error');
                }
            }

        } catch (error) {
            console.error(error);
        }
    });

});