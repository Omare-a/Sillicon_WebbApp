const lengthValidator = (e, minLenght = 2) => {
    if (value.lenght >= minLenght)
        return true


    return false
}

const compareValidator = (value, compareValue) => {
    if (value === compareValue)
        return true
    return false
}

const checkValidation = (e) => {
    if (e.checked)
        return true
    return false
}