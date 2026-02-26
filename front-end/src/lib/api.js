const API_BASE = "http://localhost:5090";

export async function fetchMembers() {
    const res = await fetch(`${API_BASE}/api/members`);
    if (res.ok) {
        return await res.json();
    }
    throw new Error("Failed to fetch members");
}

export async function fetchBooks() {
    const res = await fetch(`${API_BASE}/api/assets/books`);
    if (res.ok) {
        return await res.json();
    }
    throw new Error("Failed to fetch books");
}

export async function fetchLaptops() {
    const res = await fetch(`${API_BASE}/api/assets/laptops`);
    if (res.ok) {
        return await res.json();
    }
    throw new Error("Failed to fetch laptops");
}

export async function addBook(data) {
    const res = await fetch(`${API_BASE}/api/assets/books`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
    });
    if (!res.ok) throw new Error("Failed to add book");
    return res.json();
}

export async function addLaptop(data) {
    const res = await fetch(`${API_BASE}/api/assets/laptops`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
    });
    if (!res.ok) throw new Error("Failed to add laptop");
    return res.json();
}

export async function deleteBook(id) {
    const res = await fetch(`${API_BASE}/api/assets/books/${id}`, { method: "DELETE" });
    if (!res.ok) throw new Error("Failed to delete book");
}

export async function deleteLaptop(id) {
    const res = await fetch(`${API_BASE}/api/assets/laptops/${id}`, { method: "DELETE" });
    if (!res.ok) throw new Error("Failed to delete laptop");
}

export async function checkoutBook(bookId, memberId) {
    const res = await fetch(`${API_BASE}/api/assets/books/checkout/${bookId}/${memberId}`, {
        method: "POST",
    });
    if (!res.ok) throw new Error("Failed to checkout book");
}

export async function checkoutLaptop(laptopId, memberId) {
    const res = await fetch(`${API_BASE}/api/assets/laptops/checkout/${laptopId}/${memberId}`, {
        method: "POST",
    });
    if (!res.ok) throw new Error("Failed to checkout laptop");
}

export async function returnBook(bookId) {
    const res = await fetch(`${API_BASE}/api/assets/books/return/${bookId}`, {
        method: "POST",
    });
    if (!res.ok) throw new Error("Failed to return book");
}

export async function returnLaptop(laptopId) {
    const res = await fetch(`${API_BASE}/api/assets/laptops/return/${laptopId}`, {
        method: "POST",
    });
    if (!res.ok) throw new Error("Failed to return laptop");
}

export async function addMember(data) {
    const res = await fetch(`${API_BASE}/api/members`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
    });
    if (!res.ok) throw new Error("Failed to add member");
    return res.json();
}

export async function deleteMember(id) {
    const res = await fetch(`${API_BASE}/api/members/${id}`, { method: "DELETE" });
    if (!res.ok) throw new Error("Failed to delete member");
}
