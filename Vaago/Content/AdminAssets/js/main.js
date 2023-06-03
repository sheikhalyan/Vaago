const html = document.documentElement;
const body = document.body;
const menuLinks = document.querySelectorAll(".admin-menu a");
const collapseBtn = document.querySelector(".admin-menu .collapse-btn");
const toggleMobileMenu = document.querySelector(".toggle-mob-menu");
const switchInput = document.querySelector(".switch input");
const switchLabel = document.querySelector(".switch label");
const switchLabelText = switchLabel.querySelector("span:last-child");
const collapsedClass = "collapsed";
const lightModeClass = "light-mode";

/*TOGGLE HEADER STATE*/
collapseBtn.addEventListener("click", function () {
  body.classList.toggle(collapsedClass);
  this.getAttribute("aria-expanded") == "true"
    ? this.setAttribute("aria-expanded", "false")
    : this.setAttribute("aria-expanded", "true");
  this.getAttribute("aria-label") == "collapse menu"
    ? this.setAttribute("aria-label", "expand menu")
    : this.setAttribute("aria-label", "collapse menu");
});

/*TOGGLE MOBILE MENU*/
toggleMobileMenu.addEventListener("click", function () {
  body.classList.toggle("mob-menu-opened");
  this.getAttribute("aria-expanded") == "true"
    ? this.setAttribute("aria-expanded", "false")
    : this.setAttribute("aria-expanded", "true");
  this.getAttribute("aria-label") == "open menu"
    ? this.setAttribute("aria-label", "close menu")
    : this.setAttribute("aria-label", "open menu");
});

/*SHOW TOOLTIP ON MENU LINK HOVER*/
for (const link of menuLinks) {
  link.addEventListener("mouseenter", function () {
    if (
      body.classList.contains(collapsedClass) &&
      window.matchMedia("(min-width: 768px)").matches
    ) {
      const tooltip = this.querySelector("span").textContent;
      this.setAttribute("title", tooltip);
    } else {
      this.removeAttribute("title");
    }
  });
}

/*TOGGLE LIGHT/DARK MODE*/
if (localStorage.getItem("dark-mode") === "false") {
  html.classList.add(lightModeClass);
  switchInput.checked = false;
  switchLabelText.textContent = "Light";
}

switchInput.addEventListener("input", function () {
  html.classList.toggle(lightModeClass);
  if (html.classList.contains(lightModeClass)) {
    switchLabelText.textContent = "Light";
    localStorage.setItem("dark-mode", "false");
  } else {
    switchLabelText.textContent = "Dark";
    localStorage.setItem("dark-mode", "true");
  }
});



function editAdminProfile() {
    $(".admin-profile-input").removeAttr("disabled");
}

function updateProfile() {
    $(".admin-profile-input").attr("disabled");
}


//// AJAX DELETE REQUEST
//$(".delete-checkbox").change(function () {
//    var itemsToDelete = [];

//    // Iterate over the checked checkboxes and collect the item IDs
//    $(".delete-checkbox:checked").each(function () {
//        var itemID = $(this).closest("tr").find("td:first-child").text();
//        itemsToDelete.push(parseInt(itemID));
//    });

//    // Send the AJAX request to delete the selected items
//    $.ajax({
//        url: '@Url.Action("DeleteSelected", "AdminMenu")',
//        type: 'POST',
//        dataType: 'json',
//        data: JSON.stringify({ items: itemsToDelete }),
//        contentType: 'application/json',
//        success: function (response) {
//            if (response.success) {
//                // Handle success, such as displaying a success message or refreshing the data grid
//                alert("Items deleted successfully.");
//                // Refresh the data grid view or perform any other necessary actions
//            } else {
//                // Handle error, such as displaying an error message
//                alert("An error occurred while deleting items.");
//            }
//        },
//        error: function () {
//            // Handle error, such as displaying an error message
//            alert("An error occurred while deleting items.");
//        }
//    });
//});


$("#updateStatusBtn").click(function () {
    var orderStatus = $("#oStatus option:selected").text();
    var orderID = $("#orderIDPlace").html();
    console.log(orderID)
    var f = {}
    f.url = '/AdminOrders/UpdateStatus';
    f.type = "POST";
    f.dataType = "json";
    f.data = JSON.stringify({ orderID: orderID, orderStatus: orderStatus });
    f.contentType = "application/json";
    f.success = () => {
        alert("success")
    }
    $.ajax(f);
    //location.href = "/AdminOrders/UpdateStatus"
});