<script>
  import { fade, fly } from 'svelte/transition';
  import { deleteBook, deleteLaptop, checkoutBook, checkoutLaptop, returnBook, returnLaptop } from '$lib/api.js';

  let { books = [], laptops = [], members = [], onUpdate } = $props();

  let activeTab = $state("all");
  let selectedMemberId = $state({});

  const displayedItems = $derived(
    activeTab === "books"
      ? books.map((b) => ({ ...b, type: "book" }))
      : activeTab === "laptops"
        ? laptops.map((l) => ({ ...l, type: "laptop" }))
        : [...books.map((b) => ({ ...b, type: "book" })), ...laptops.map((l) => ({ ...l, type: "laptop" }))]
  );

  function getMemberName(memberId) {
    const member = members.find((m) => m.id === memberId);
    return member ? member.name : "N/A";
  }

  async function handleDelete(type, id) {
    try {
      if (type === "book") {
        await deleteBook(id);
      } else {
        await deleteLaptop(id);
      }
      onUpdate?.(`${type} deleted successfully`);
    } catch (error) {
      onUpdate?.(`Failed to delete ${type}`);
    }
  }

  async function handleCheckout(type, itemId, memberId) {
    if (!memberId) {
      onUpdate?.("Please select a member");
      return;
    }

    try {
      if (type === "book") {
        await checkoutBook(itemId, memberId);
      } else {
        await checkoutLaptop(itemId, memberId);
      }
      selectedMemberId[itemId] = "";
      onUpdate?.(`${type} checked out successfully`);
    } catch (error) {
      onUpdate?.(`Failed to checkout ${type}`);
    }
  }

  async function handleReturn(type, itemId) {
    try {
      if (type === "book") {
        await returnBook(itemId);
      } else {
        await returnLaptop(itemId);
      }
      onUpdate?.(`${type} returned successfully`);
    } catch (error) {
      onUpdate?.(`Failed to return ${type}`);
    }
  }
</script>

<div class="bg-card border border-border rounded-lg p-6">
  <h2 class="text-2xl font-semibold mb-4 text-foreground">Assets</h2>
  
  <div class="flex gap-2 mb-4">
    <button 
      onclick={() => (activeTab = "all")}
      class="px-4 py-2 rounded-md transition-colors {activeTab === 'all' ? 'bg-primary text-primary-foreground' : 'bg-secondary text-secondary-foreground hover:bg-secondary/80'}"
    >
      All
    </button>
    <button 
      onclick={() => (activeTab = "books")}
      class="px-4 py-2 rounded-md transition-colors {activeTab === 'books' ? 'bg-primary text-primary-foreground' : 'bg-secondary text-secondary-foreground hover:bg-secondary/80'}"
    >
      Books
    </button>
    <button 
      onclick={() => (activeTab = "laptops")}
      class="px-4 py-2 rounded-md transition-colors {activeTab === 'laptops' ? 'bg-primary text-primary-foreground' : 'bg-secondary text-secondary-foreground hover:bg-secondary/80'}"
    >
      Laptops
    </button>
  </div>

  <div class="overflow-x-auto">
    <table class="w-full">
      <thead>
        <tr class="border-b border-border">
          <th class="text-left py-3 px-4 font-medium text-foreground">ID</th>
          <th class="text-left py-3 px-4 font-medium text-foreground">Type</th>
          <th class="text-left py-3 px-4 font-medium text-foreground">Name</th>
          <th class="text-left py-3 px-4 font-medium text-foreground">Details</th>
          <th class="text-left py-3 px-4 font-medium text-foreground">Condition</th>
          <th class="text-left py-3 px-4 font-medium text-foreground">Checked Out To</th>
          <th class="text-left py-3 px-4 font-medium text-foreground">Actions</th>
        </tr>
      </thead>
      <tbody>
        {#each displayedItems as item (item.id + item.type)}
          <tr 
            class="border-b border-border hover:bg-muted/50 transition-colors"
            transition:fly={{ y: 20, duration: 300 }}
          >
            <td class="py-3 px-4 text-foreground">{item.id}</td>
            <td class="py-3 px-4 text-foreground capitalize">{item.type}</td>
            <td class="py-3 px-4 text-foreground">{item.name}</td>
            <td class="py-3 px-4 text-muted-foreground text-sm">
              {#if item.type === "book"}
                Author: {item.author}
              {:else}
                SN: {item.serialNumber}, OS: {item.os}
              {/if}
            </td>
            <td class="py-3 px-4 text-foreground">{item.itemCondition}</td>
            <td class="py-3 px-4 text-foreground">
              {item.memberId ? getMemberName(item.memberId) : "Available"}
            </td>
            <td class="py-3 px-4">
              <div class="flex gap-2 items-center">
                {#if item.memberId}
                  <button 
                    onclick={() => handleReturn(item.type, item.id)}
                    class="px-3 py-1 bg-primary text-primary-foreground rounded text-sm hover:bg-primary/90 transition-colors"
                  >
                    Return
                  </button>
                {:else}
                  <select 
                    bind:value={selectedMemberId[item.id]}
                    class="px-2 py-1 bg-background border border-input rounded text-sm focus:outline-none focus:ring-2 focus:ring-ring"
                  >
                    <option value="">Select member</option>
                    {#each members as member}
                      <option value={member.id}>{member.name}</option>
                    {/each}
                  </select>
                  <button 
                    onclick={() => handleCheckout(item.type, item.id, selectedMemberId[item.id])}
                    class="px-3 py-1 bg-primary text-primary-foreground rounded text-sm hover:bg-primary/90 transition-colors"
                  >
                    Checkout
                  </button>
                {/if}
                <button 
                  onclick={() => handleDelete(item.type, item.id)}
                  class="px-3 py-1 bg-destructive text-primary-foreground rounded text-sm hover:bg-destructive/90 transition-colors"
                >
                  Delete
                </button>
              </div>
            </td>
          </tr>
        {/each}
      </tbody>
    </table>
  </div>
</div>
