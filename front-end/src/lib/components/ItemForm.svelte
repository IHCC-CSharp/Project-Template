<script>
    import { slide } from 'svelte/transition';
    import { addBook, addLaptop } from '$lib/api.js';

    let { onSuccess } = $props();

    let itemType = $state("book");
    let itemName = $state("");
    let itemCondition = $state(0);
    let bookAuthor = $state("");
    let laptopSerial = $state("");
    let laptopOs = $state("");
    let loading = $state(false);

    async function handleSubmit(event) {
        event.preventDefault();
        loading = true;

        try {
            if (itemType === "book") {
                await addBook({
                    name: itemName,
                    itemCondition: Number(itemCondition),
                    author: bookAuthor
                });
            } else {
                await addLaptop({
                    name: itemName,
                    itemCondition: Number(itemCondition),
                    serialNumber: laptopSerial,
                    os: laptopOs
                });
            }

            // Reset form
            itemName = "";
            itemCondition = 0;
            bookAuthor = "";
            laptopSerial = "";
            laptopOs = "";

            onSuccess?.(`${itemType} added successfully`);
        } catch (error) {
            onSuccess?.(`Failed to add ${itemType}`);
        } finally {
            loading = false;
        }
    }
</script>

<div class="bg-card border border-border rounded-lg p-6">
    <h2 class="text-2xl font-semibold mb-4 text-foreground">Add New Item</h2>

    <form onsubmit={handleSubmit} class="space-y-4">
        <div>
            <label class="block text-sm font-medium text-foreground mb-1" for="itemType">
                Type
            </label>
            <select
                    bind:value={itemType}
                    id="itemType"
                    class="w-full px-3 py-2 bg-background border border-input rounded-md focus:outline-none focus:ring-2 focus:ring-ring"
            >
                <option value="book">Book</option>
                <option value="laptop">Laptop</option>
            </select>
        </div>

        <div>
            <label class="block text-sm font-medium text-foreground mb-1" for="itemName">
                Name
            </label>
            <input
                    type="text"
                    bind:value={itemName}
                    id="itemName"
                    required
                    class="w-full px-3 py-2 bg-background border border-input rounded-md focus:outline-none focus:ring-2 focus:ring-ring"
            />
        </div>

        <div>
            <label class="block text-sm font-medium text-foreground mb-1" for="itemCondition">
                Condition (0-5)
            </label>
            <input
                    type="number"
                    bind:value={itemCondition}
                    id="itemCondition"
                    min="0"
                    max="5"
                    class="w-full px-3 py-2 bg-background border border-input rounded-md focus:outline-none focus:ring-2 focus:ring-ring"
            />
        </div>

        {#if itemType === "book"}
            <div transition:slide>
                <label class="block text-sm font-medium text-foreground mb-1" for="bookAuthor">
                    Author
                </label>
                <input
                        type="text"
                        bind:value={bookAuthor}
                        id="bookAuthor"
                        required
                        class="w-full px-3 py-2 bg-background border border-input rounded-md focus:outline-none focus:ring-2 focus:ring-ring"
                />
            </div>
        {:else}
            <div transition:slide>
                <label class="block text-sm font-medium text-foreground mb-1" for="laptopSerial">
                    Serial Number
                </label>
                <input
                        type="text"
                        bind:value={laptopSerial}
                        id="laptopSerial"
                        required
                        class="w-full px-3 py-2 bg-background border border-input rounded-md focus:outline-none focus:ring-2 focus:ring-ring"
                />
            </div>
            <div transition:slide>
                <label class="block text-sm font-medium text-foreground mb-1" for="laptopOs">
                    OS
                </label>
                <input
                        type="text"
                        bind:value={laptopOs}
                        id="laptopOs"
                        required
                        class="w-full px-3 py-2 bg-background border border-input rounded-md focus:outline-none focus:ring-2 focus:ring-ring"
                />
            </div>
        {/if}

        <button
                type="submit"
                disabled={loading}
                class="w-full bg-primary text-primary-foreground px-4 py-2 rounded-md hover:bg-primary/90 transition-colors disabled:opacity-50"
        >
            {loading ? 'Adding...' : `Add ${itemType}`}
        </button>
    </form>
</div>
