function clearNode(node) {
    let elements = node.childNodes;
    let description = elements[3].value;
    let chooseBlock = elements[9];
    let enterBlock = elements[11];

    if (description !== null) {
        description = null;
    }

    if (chooseBlock.childNodes.length > 6) {
        leaveOnlyFirst(chooseBlock);
    }

    enterBlock.hidden = true;
    enterBlock.firstElementChild.value = '';
}

// Leave first two variants and remove another

function leaveOnlyFirst(parent) {
    parent.hidden = true;
    parent.childNodes[1].firstElementChild.value = '';
    parent.childNodes[3].firstElementChild.value = '';

    for (let i = 4; i < parent.childNodes.length - 2; i++) {
        let childNode = parent.childNodes[i];
        if (childNode.id !== undefined) {
            if (childNode !== parent.lastChild) {
                childNode.remove();
            }
        }
    }
}

// Adding new question

function addQuestion() {
    let question = document.getElementsByClassName('question');
    let questionTmpl = question[0].cloneNode(true);
    let addQuestionBtn = document.getElementsByClassName('btn-add-quest')[0];
    let test = document.getElementsByName('test')[0];
    clearNode(questionTmpl);

    let removeBtn = createRemoveButton('btn-remove-question');

    questionTmpl.insertBefore(removeBtn, questionTmpl.firstChild);
    test.insertBefore(questionTmpl, addQuestionBtn);
    removeBtn.addEventListener("click", remove);
}

// Creating remove button

function createRemoveButton(className) {
    let button = document.createElement('span');
    button.innerText = 'X';
    button.classList.add(className);
    return button;
}

// Adding new variant of answer

function addVariant() {
    let i = getIndexFromVariant.call(this);
    let variants = document.getElementsByClassName('variant');
    let variantTmpl = variants[i].cloneNode(true);
    variantTmpl.children[0].value = '';
    let addVariantBtn = document.getElementsByClassName('btn-add-answer')[i];
    let allVariants = document.getElementsByClassName('choose-answer')[i];

    let removeBtn = createRemoveButton('btn-remove-answer');

    variantTmpl.appendChild(removeBtn);
    allVariants.insertBefore(variantTmpl, addVariantBtn);
    removeBtn.addEventListener("click", remove);
}

// Remove question or variant

function remove() {
    this.parentElement.remove();
}

// Displaying data depending on type of the task

function toggleDisplay() {
    let i = getIndexFromSelect.call(this);
    let typeSelect = document.getElementsByName('types')[i];
    let selectedOption = typeSelect.options[typeSelect.selectedIndex].value;
    let chooseAnswer = document.getElementsByClassName('choose-answer')[i];
    let enterAnswer = document.getElementsByClassName('enter-answer')[i];

    if (selectedOption === "enter") {
        chooseAnswer.hidden = true;
        enterAnswer.hidden = false;
    }
    else {
        enterAnswer.hidden = true;
        chooseAnswer.hidden = false;
    }
}

//Getting index of the question

function getIndexFromSelect() {
    let question = this.parentNode;
    let form = question.parentNode;
    let array = Array.from(form.children);
    let index = array.indexOf(question);
    return index;
}

function getIndexFromVariant() {
    let question = this.parentNode.parentNode;
    let form = question.parentNode;
    let array = Array.from(form.children);
    let index = array.indexOf(question);
    return index;
}