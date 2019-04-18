var varToStringify = #;

function removeCircular(me, parents) {
	if (parents == null) parents = [];
	if (typeof me !== "object" || me instanceof HTMLElement) return me;
	if (parents.includes(me) || parents.length > 10) return null;
	for (let i in me) {
		if (Number(i) > 3) 
			me[i] = undefined;
		else 
			me[i] = removeCircular(me[i], [...parents, me])
	}
	return me;
}

JSON.stringify(removeCircular(varToStringify));