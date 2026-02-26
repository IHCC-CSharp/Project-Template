<script lang="ts">
    import '$lib/api-client'; 
    import { getApiAssetsBooks, postApiAssetsBooksCheckoutByIdByMemberId } from '$lib/api/sdk.gen';
    import type { Book } from '$lib/api/types.gen';

    let books = $state<Book[]>([]);

    async function loadBooks() {
        const { data } = await getApiAssetsBooks();
        if (data) books = data;
    }

    async function checkoutBook(bookId: number, memberId: number) {

        await postApiAssetsBooksCheckoutByIdByMemberId({
            path: { 
                id: bookId, 
                memberId: memberId 
            }
        });
        loadBooks();
    }

    $effect(() => { loadBooks(); });
</script>

<h2>Book Inventory</h2>
<div class="grid">
    {#each books as book}
        <div class="card">
            <h3>{book.name}</h3>
            <p>Author: {book.author}</p>
            <p>Status: {book.currentMemberId ? 'Checked Out' : 'Available'}</p>
            
            {#if !book.currentMemberId}
                <button onclick={() => checkoutBook(book.id!, 1)}>Quick Checkout (ID: 1)</button>
            {/if}
        </div>
    {/each}
</div>