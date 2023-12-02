function selectCategory(sel) {
    sel.removeAttribute("asp-for")
    let parentCategory = sel.options[sel.selectedIndex];
    for (let i = 0; i < sel.length; i++) {
        let childItem = document.getElementById(`parent_${sel[i].value}`)
        if (childItem)
            childItem.remove();
    }

    if (parentCategory.value != 0)
        getItems(parentCategory, sel)
}

function getItems(parentCategory, sel) {
    const url = `/api/category/GetCategoriesByParent/${parentCategory.value}`;
    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw response;
            }
            return response.json();
        })
        .then(json => {
            createItem(json, parentCategory, sel)
        })
        .catch(response => console.log(response));
}

function createItem(lst, parentCategory, sel) {
    if (lst.children.length < 1) {
        sel.setAttribute("name", "CategoryId");
        return false;
    }
    let categoryBox = document.getElementById('category_items');
    let parenetItem = document.getElementById(`parent_${lst.parentId}`)
    let parentTitle = parentCategory.text;
    let parentValue = parentCategory.value;

    let newParentDiv = document.createElement("div");
    newParentDiv.id = `parent_${parentValue}`;

    if (parenetItem)
        parenetItem.append(newParentDiv)

    let div = document.createElement("div");
    div.className = "form-group";

    let label = document.createElement("label");
    label.className = "control-label";
    label.textContent = `دسته بندی ${parentTitle}`;
    label.setAttribute("name", "CategoryId");

    let select = document.createElement("select");
    select.className = "form-control";
    select.setAttribute("onchange", "selectCategory(this)");

    let mainOption = document.createElement("option");
    mainOption.value = "0"
    mainOption.textContent = `انتخاب دستی بندی ${parentTitle}`;

    select.append(mainOption)
    newParentDiv.append(div)
    div.append(label, select)

    lst.children.forEach(MyResult)
    function MyResult(item) {
        let option = document.createElement("option");
        option.value = item.id;
        option.textContent = item.title;
        select.append(option)
    }
    if (parenetItem)
        categoryBox.appendChild(parenetItem)
    else
        categoryBox.appendChild(newParentDiv)
}