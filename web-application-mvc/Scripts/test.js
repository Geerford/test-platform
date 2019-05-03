function addVariant() {
    let variants = document.getElementsByClassName('variant');
    let variantTmpl = variants[0].cloneNode(true);
    let addVariantBtn = document.getElementsByClassName('btn-add-answer')[0];
    let allVariants = document.getElementsByClassName('choose-answer')[0];
    allVariants.insertBefore(variantTmpl, addVariantBtn);
}

function addQuestion() {
    let question = document.getElementsByClassName('question');
    let questionTmpl = question[0].cloneNode(true);
    let addQuestionBtn = document.getElementsByClassName('btn-add-quest')[0];
    let test = document.getElementsByName('test')[0];
    test.insertBefore(questionTmpl, addQuestionBtn);
}

function getIndex() {
    let question = this.parentNode;
    let form = this.parentNode.parentNode;
    let array = Array.from(form.childNodes);
    let index = array.indexOf(question);
    return index;
}

function toggleDisplay() {
    let i = getIndex.call(this);
    if (i === 1) {
        i = i - 1;
    }
    else {
        i = i - 2;
    }
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