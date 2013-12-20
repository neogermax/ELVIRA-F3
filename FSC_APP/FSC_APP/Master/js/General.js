function textboxMultilineMaxNumber(txt, maxLen) {
    try {
        if (txt.value.length > (maxLen - 1))
            return false;
    } catch (e) {
    }
} 