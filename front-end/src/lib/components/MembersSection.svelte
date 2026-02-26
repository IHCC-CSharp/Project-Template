<script>
    import { fly } from 'svelte/transition';
    import { addMember, deleteMember } from '$lib/api.js';

    let { members = [], onUpdate } = $props();

    let memberName = "";
    let memberEmail = "";
    let loading = false;

    async function handleSubmit(event) {
        event.preventDefault();
        loading = true;

        try {
            await addMember({ name: memberName, email: memberEmail });
            memberName = "";
            memberEmail = "";
            onUpdate?.("Member added successfully");
        } catch (error) {
            onUpdate?.("Failed to add member");
        } finally {
            loading = false;
        }
    }

    async function handleDelete(id) {
        try {
            await deleteMember(id);
            onUpdate?.("Member deleted successfully");
        } catch (error) {
            onUpdate?.("Failed to delete member");
        }
    }
</script>

<div class="bg-card border border-border rounded-lg p-6">
    <h2 class="text-2xl font-semibold mb-4 text-foreground">Members</h2>

    <form onsubmit={handleSubmit} class="grid grid-cols-1 md:grid-cols-3 gap-4 mb-6">
        <div>
            <label class="block text-sm font-medium text-foreground mb-1" for="memberName">
                Name
            </label>
            <input
                    id="memberName"
                    type="text"
                    bind:value={memberName}
                    required
                    class="w-full px-3 py-2 bg-background border border-input rounded-md focus:outline-none focus:ring-2 focus:ring-ring"
            />
        </div>

        <div>
            <label class="block text-sm font-medium text-foreground mb-1" for="memberEmail">
                Email
            </label>
            <input
                    id="memberEmail"
                    type="email"
                    bind:value={memberEmail}
                    required
                    class="w-full px-3 py-2 bg-background border border-input rounded-md focus:outline-none focus:ring-2 focus:ring-ring"
            />
        </div>

        <div class="flex items-end">
            <button
                    type="submit"
                    disabled={loading}
                    class="w-full bg-primary text-primary-foreground px-4 py-2 rounded-md hover:bg-primary/90 transition-colors disabled:opacity-50"
            >
                {loading ? 'Adding...' : 'Add Member'}
            </button>
        </div>
    </form>

    <div class="overflow-x-auto">
        <table class="w-full">
            <thead>
            <tr class="border-b border-border">
                <th class="text-left py-3 px-4 font-medium text-foreground">ID</th>
                <th class="text-left py-3 px-4 font-medium text-foreground">Name</th>
                <th class="text-left py-3 px-4 font-medium text-foreground">Email</th>
                <th class="text-left py-3 px-4 font-medium text-foreground">Actions</th>
            </tr>
            </thead>
            <tbody>
            {#each members as member (member.id)}
                <tr
                        class="border-b border-border hover:bg-muted/50 transition-colors"
                        transition:fly={{ y: 20, duration: 300 }}
                >
                    <td class="py-3 px-4 text-foreground">{member.id}</td>
                    <td class="py-3 px-4 text-foreground">{member.name}</td>
                    <td class="py-3 px-4 text-foreground">{member.email}</td>
                    <td class="py-3 px-4">
                        <button
                                onclick={() => handleDelete(member.id)}
                                class="px-3 py-1 bg-destructive text-primary-foreground rounded text-sm hover:bg-destructive/90 transition-colors"
                        >
                            Delete
                        </button>
                    </td>
                </tr>
            {/each}
            </tbody>
        </table>
    </div>
</div>
