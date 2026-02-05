function toggleDropdown(id) {
        let menu = document.getElementById(id);
        let alreadyOpen = menu.classList.contains('show');
        // Close all other menus first
        document.querySelectorAll('.dropdown-menu').forEach(m => m.classList.remove('show'));
        // Toggle current
        if (!alreadyOpen) menu.classList.add('show');
    }

    function filterList(input) {
        let filter = input.value.toLowerCase();
        let rows = input.closest('.dropdown-menu').querySelectorAll('.check-row');
        rows.forEach(row => {
            let text = row.textContent.toLowerCase();
            row.style.display = text.includes(filter) ? "flex" : "none";
        });
    }

    function handleCheck(cb, tagAreaId) {
        const tagArea = document.getElementById(tagAreaId);
        const valId = 'tag-' + cb.value.replace(/\s+/g, '-').toLowerCase();
        
        if (cb.checked) {
            let tag = document.createElement('div');
            tag.className = 'tag';
            tag.id = valId;
            tag.innerHTML = `${cb.value} <i class="fa-solid fa-xmark" onclick="removeTag('${cb.value}', '${tagAreaId}')"></i>`;
            tagArea.appendChild(tag);
        } else {
            let existingTag = document.getElementById(valId);
            if(existingTag) existingTag.remove();
        }
    }

    function removeTag(val, tagAreaId) {
        // Find the checkbox in the specific dropdown parent and uncheck it
        const menu = document.getElementById(tagAreaId).closest('.dropdown-menu');
        const checkboxes = menu.querySelectorAll('.check-row input');
        checkboxes.forEach(c => { if(c.value === val) c.checked = false; });
        
        // Remove the visual tag
        const valId = 'tag-' + val.replace(/\s+/g, '-').toLowerCase();
        document.getElementById(valId).remove();
    }

    window.onclick = function(e) {
        if (!e.target.closest('.filter-item')) {
            document.querySelectorAll('.dropdown-menu').forEach(m => m.classList.remove('show'));
        }
    }